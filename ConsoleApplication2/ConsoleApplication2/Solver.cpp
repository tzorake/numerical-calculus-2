#include <iostream>
#include <iomanip>
#include <cmath>

class Solver {
private:
	static const int			m = 15											;

	std::pair<double, double>	interval										;
	int							N												;
	double						h												;

	int							gridLength										;
	double*						x												;
	double*						y												;

	double						psi												;

public:
	Solver(std::pair<double, double> interval, int N) {
		this->interval			= interval										;
		this->N					= N												;
		this->h					= 1. / N										;
		this->gridLength		= N + 1											;
		this->x					= (double*)malloc(gridLength * sizeof(double))	;
		this->y					= (double*)malloc(gridLength * sizeof(double))	;

		this->psi				= 0												;

		fillGrid()																;
	}

	void fillGrid() {
		for (int i = 0; i < gridLength; i++)
			x[i] = interval.first + i * h;
	}

	void solve(double mu1, double mu2) {
		y[0] = mu1;
		y[N] = mu2;

		double* alpha = (double*)malloc(gridLength * sizeof(double));
		double* beta = (double*)malloc(gridLength * sizeof(double));

		alpha[0] = 0;
		beta[0] = U(interval.first);

		for (int i = 0; i < N; i++) {
			double A = 1. / pow(h, 2.);
			double B = 1. / pow(h, 2.);
			double C = 2. / pow(h, 2.) + q(x[i]);
			double F = f(x[i]);

			alpha[i + 1] = B / (C - A * alpha[i]);
			beta[i + 1] = (A * beta[i] + F) / (C - A * alpha[i]);
		}

		for (int i = N - 1; i >= 0; i--)
			y[i] = alpha[i + 1] * y[i + 1] + beta[i + 1];

		for (int i = 1; i < gridLength - 1; i++)
			if (abs(y[i] - U(x[i])) > psi) psi = abs(y[i] - U(x[i]));
	}

	void info() {
		std::cout		<< "\tSolution of the problem:"											<< std::endl;
		std::cout		<< "[a, b]\t=\t[" << interval.first << ", " << interval.second << "]"	<< std::endl
						<< " h\t=\t "								<< h						<< std::endl
						<< " y\t=\t";

		std::cout		<< "{";
		for (int i = 0; i < gridLength; i++)
			!(gridLength - i - 1 == 0 ) ?	std::cout << std::setprecision(3) << y[i] << ", " : 
											std::cout << std::setprecision(3) << y[i];
		std::cout		<< "}";

		std::cout																				<< std::endl 
						<< " psi\t=\t " << psi													<< std::endl;
	}

	double q(double x) {
		return 15. * x;
	}
	double f(double x) {
		return 15. * x * (pow(x, 3) + 1./15. * x + 15.) - 6. * x;
	}
	static double U(double x) {
		return pow(x, 3.) + 1./15. * x + 15.;
	}
};

void main() {
	int n = 10;
	double mu1 = 15.;
	double mu2 = 16.0667;

	// here we create our solution finders which have different 'n'. 
	// it need to test how changes 'psi' when 'n' is increasing.
	//						 interval  N
	Solver* s1 = new Solver( { 0, 1 }, 10 );
	s1->solve(mu1, mu2);
	s1->info();
	// delete the solver
	delete s1;
	//						 interval  N
	Solver* s2 = new Solver( { 0, 1 }, 100);
	s2->solve(mu1, mu2);
	s2->info();
	// delete the solver
	delete s2;
}