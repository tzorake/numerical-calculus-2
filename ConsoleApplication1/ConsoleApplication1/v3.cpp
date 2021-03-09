#include <iostream>

class EulerSolver {
private:
	static constexpr double			m = 15.												;
	static constexpr double			g = 1.												;

	std::pair<double, double>		interval											;
	double							y0													;
	int								N													;
	double*							t													;
	int								gridLength											;
	double							h													;

	double*							y													;
	double*							z													;
	double							psi													;
public:
	EulerSolver(std::pair<double, double> interval, double y0, int N) {
		this->interval				= interval											;
		this->y0					= y0												;
		this->N						= N													;
		this->gridLength			= N + 1												;
		this->t						= (double*)malloc(gridLength * sizeof(double))		;
		this->h						= (interval.second - interval.first) / ((double)N)	;

		this->y						= (double*)malloc(gridLength * sizeof(double))		;
		this->z						= (double*)malloc(gridLength * sizeof(double))		;
		this->psi					= 0													;

		fillGrid()																		;
	}

	void fillGrid() {
		for (int i = 0; i < gridLength; i++)
			t[i] = interval.first + i * h;
	}

	void solve() {
		y[0] = y0;
		for (int i = 0; i < N; i++) {
			y[i + 1] = y[i] + h * f(t[i], y[i]);		
		}

		for (int i = 0; i < gridLength; i++) {
			z[i] = abs(y[i] - U(t[i]));
			if (z[i] > psi) psi = z[i];
		}
	}

	static double U(double x) {
		return m / 2. * x + g / x;
	}

	double f(double x, double y) {
		return (m * x - y) / x;
	}

	void info() {
		std::cout	<< "\tEuler method"														<< std::endl;
		std::cout	<< "[a, b]\t=\t["	<< interval.first << ", " << interval.second << "]" << std::endl
					<< " U("			<< interval.first << ")\t=\t " << y0				<< std::endl
					<< " h\t=\t "		<< h												<< std::endl;
		std::cout	<< " y_n\t=\t "		<< y[N]												<< std::endl;
		std::cout	<< " psi\t=\t "		<< psi												<< std::endl;
	}
};

class RungeKuttaSolver2 {
private:
	static constexpr double			m = 15.												;
	static constexpr double			g = 1.												;

	std::pair<double, double>		interval											;
	double							y0													;
	int								N													;
	double*							t													;
	int								gridLength											;
	double							h													;

	double*							y													;
	double*							z													;
	double							psi													;

	double							omega;
	double							alpha;
public:
	RungeKuttaSolver2(std::pair<double, double> interval, double y0, int N, double omega, double alpha) {
		this->interval				= interval											;
		this->y0					= y0												;
		this->N						= N													;
		this->gridLength			= N + 1												;
		this->t						= (double*)malloc(gridLength * sizeof(double))		;
		this->h						= (interval.second - interval.first) / ((double)N)	;

		this->y						= (double*)malloc(gridLength * sizeof(double))		;
		this->z						= (double*)malloc(gridLength * sizeof(double))		;
		this->psi					= 0													;

		this->omega					= omega												;
		this->alpha					= alpha												;

		fillGrid()																		;
	}

	void fillGrid() {
		for (int i = 0; i < gridLength; i++)
			t[i] = interval.first + i * h;
	}

	void solve() {
		y[0] = y0;
		for (int i = 0; i < N; i++) {
			y[i + 1] = (1 - omega) * f(t[i], y[i]) + omega * f(t[i] + 
				alpha * h, y[i] + alpha * h * f(t[i], y[i]))*h + y[i];
		}

		for (int i = 0; i < gridLength; i++) {
			z[i] = abs(y[i] - U(t[i]));
			if (z[i] > psi) psi = z[i];
		}
	}

	static double U(double x) {
		return m / 2. * x + g / x;
	}

	double f(double x, double u) {
		return (m * x - u) / x;
	}

	void info() {
		std::cout	<< "\tRunge-Kutta method (second-order)"								<< std::endl;
		std::cout	<< "[a, b]\t=\t["	<< interval.first << ", " << interval.second << "]"	<< std::endl
					<< " U("			<< interval.first << ")\t=\t " << y0				<< std::endl
					<< " h\t=\t "		<< h												<< std::endl;
		std::cout	<< " y_n\t=\t "		<< y[N]												<< std::endl;
		std::cout	<< " psi\t=\t "		<< psi												<< std::endl;
		std::cout	<< " omega\t=\t "	<< omega											<< std::endl;
		std::cout	<< " alpha\t=\t "	<< alpha											<< std::endl;
	}
};

class RungeKuttaSolver4 {
private:
	static constexpr double			m = 15.												;
	static constexpr double			g = 1.												;

	std::pair<double, double>		interval											;
	double							y0													;
	int								N													;
	double*							t													;
	int								gridLength											;
	double							h													;

	double*							y													;
	double*							z													;
	double							psi													;
public:
	RungeKuttaSolver4(std::pair<double, double> interval, double y0, int N) {
		this->interval				= interval											;
		this->y0					= y0												;
		this->N						= N													;
		this->gridLength			= N + 1												;
		this->t						= (double*)malloc(gridLength * sizeof(double))		;
		this->h						= (interval.second - interval.first) / ((double)N)	;

		this->y						= (double*)malloc(gridLength * sizeof(double))		;
		this->z						= (double*)malloc(gridLength * sizeof(double))		;
		this->psi					= 0													;

		fillGrid()																		;
	}

	void fillGrid() {
		for (int i = 0; i < gridLength; i++)
			t[i] = interval.first + i * h;
	}

	void solve() {
		y[0] = y0;
		for (int i = 0; i < N; i++) {
			double k1 = f(t[i], y[i]);
			double k2 = f(t[i] + h / 2., y[i] + h * k1 / 2.);
			double k3 = f(t[i] + h / 2., y[i] + h * k2 / 2.);
			double k4 = f(t[i] + h, y[i] + h * k3);

			y[i + 1] = y[i] + 1. / 6. * (k1 + 2. * k2 + 2. * k3 + k4) * h;
		}

		for (int i = 0; i < gridLength; i++) {
			z[i] = abs(y[i] - U(t[i]));
			if (z[i] > psi) psi = z[i];
		}
	}

	static double U(double x) {
		return m / 2. * x + g / x;
	}

	double f(double x, double u) {
		return (m * x - u) / x;
	}

	void info() {
		std::cout	<< "\tRunge-Kutta method (forth-order)"									<< std::endl;
		std::cout	<< "[a, b]\t=\t["	<< interval.first << ", " << interval.second << "]"	<< std::endl
					<< " U("			<< interval.first << ")\t=\t " << y0				<< std::endl
					<< " h\t=\t "		<< h												<< std::endl;
		std::cout	<< " y_n\t=\t "		<< y[N]												<< std::endl;
		std::cout	<< " psi\t=\t "		<< psi												<< std::endl;
	}
};

void main() {
	int n = 10;
	//								    interval  initial value      N 
	EulerSolver* es1 = new EulerSolver( { 1, 2 }, EulerSolver::U(1), n);
	// solve the ODE and print information about the ODE.
	es1->solve();
	es1->info();
	// delete the solver
	delete es1;
	std::cout << std::endl;
	//												 interval  initial value			N	alpha	omega
	RungeKuttaSolver2* rks2 = new RungeKuttaSolver2( { 1, 2 }, RungeKuttaSolver2::U(1), n,	1,		0.5);
	// solve the ODE and print information about the ODE.
	rks2->solve();
	rks2->info();
	// delete the solver
	delete rks2;
	std::cout << std::endl;
	//												 interval  initial value			N
	RungeKuttaSolver4* rks4 = new RungeKuttaSolver4( { 1, 2 }, RungeKuttaSolver4::U(1), n);
	// solve the ODE and print information about the ODE.
	rks4->solve();
	rks4->info();
	// delete the solver
	delete rks4;
}