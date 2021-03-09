#include <iostream>
#include <cmath>
#include <string>

using namespace std;


class InitialValues {
public:
	static double mu1(double x) {
		return pow(x, 3.0) + x + 2.0;
	}

	static double mu2(double x) {
		return 2.0 * pow(1.0, 3.0) + pow(x, 3.0) + 2.0 * 1.0 + x + 2.0;
	}

	static double mu3(double x) {
		return 2.0 * pow(x, 3.0) + 2.0 * x + 2.0;
	}

	static double mu4(double x) {
		return 2.0 * pow(x, 3.0) + pow(1.0, 3.0) + 2.0 * x + 1.0 + 2.0;
	}
};

class Solver {
public:
	double eps;

	pair<double, double> interval;
	double h;

	int gridSize;
	pair<double, double>** x;
	double** y;

	double omega;

	double** z;
	double psi;
	int k;

	Solver(pair<double, double> interval, int N, double epsilon) {
		this->eps = epsilon;
		this->interval = interval;
		this->h = 1. / (double)N;

		this->gridSize = N + 1;

		this->x = new pair<double, double>*[gridSize];
		for (int i = 0; i < gridSize; i++)
			this->x[i] = new pair<double, double>[gridSize];
		for (int i = 0; i < gridSize; i++)
			for (int j = 0; j < gridSize; j++)
				this->x[i][j] = { interval.first + i * h, interval.first + j * h };
	}

	void solve(double omegaValue) {
		this->omega = omegaValue;
		// assign and fill 'y' grid
		this->y = new double* [gridSize];
		for (int i = 0; i < gridSize; i++)
			this->y[i] = new double[gridSize];
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				if (x[i][j].first == 0) {
					this->y[i][j] = InitialValues::mu1(x[i][j].second);
				}
				else if (x[i][j].first == interval.second) {
					this->y[i][j] = InitialValues::mu2(x[i][j].second);
				}
				else if (x[i][j].second == 0) {
					this->y[i][j] = InitialValues::mu3(x[i][j].first);
				}
				else if (x[i][j].second == interval.second) {
					this->y[i][j] = InitialValues::mu4(x[i][j].first);
				}
				else {
					this->y[i][j] = 0;
				}
			}
		}

		// assign and fill 'z' grid
		this->z = new double* [gridSize];
		for (int i = 0; i < gridSize; i++)
			this->z[i] = new double[gridSize];
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				z[i][j] = 0;
			}
		}
		this->psi = 0;
		this->k = 0;
		// create and copy 'y_old' grid from 'y' grid
		double** y_old = new double*[gridSize];
		for (int i = 0; i < gridSize; i++)
			y_old[i] = new double[gridSize];
		// over-relax. method
		do {
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					y_old[i][j] = y[i][j];
				}
			}
			for (int i = 1; i < gridSize - 1; i++)
				for (int j = 1; j < gridSize - 1; j++)
					y[i][j] = omega * (y[i - 1][j] + y[i][j - 1] +  y_old[i + 1][j] + y_old[i][j + 1] + h*h*f(x[i][j]))/4.0 + (1 - omega) * y_old[i][j];
			k++;
		} while (customMax(y, y_old, {gridSize, gridSize}) >= eps);
		// find 'psi' value
		for (int i = 1; i < gridSize - 1; i++) {
			for (int j = 1; j < gridSize - 1; j++) {
				z[i][j] = abs(y[i][j] - U(x[i][j]));
				if (z[i][j] > psi) psi = z[i][j];
			}
		}
	}

	void showGrids(string type) {
		if (type.compare("x") == 0) {
			cout << "x: \n";
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					cout << customRound(x[i][j].first, 2) << ", " << customRound(x[i][j].second, 2) << "\t";
				}
				cout << "\n";
			}
			cout << "\n";
		}
		else if (type.compare("y") == 0) {
			cout << "y: \n";
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					cout << customRound(y[i][j], 2) << "\t";
				}
				cout << "\n";
			}
			cout << "\n";
		}
		else if (type.compare("z") == 0) {
			cout << "z: \n";
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					cout << customRound(z[i][j], 2) << "\t";
				}
				cout << "\n";
			}
			cout << "\n";
		}
		
	}

	static double U(pair<double, double> x) {
		return 2.0 * pow(x.first, 3.0) + pow(x.second, 3.0) + 2.0 * x.first + x.second + 2.0;
	}

	double f(pair<double, double> x) {
		return -12 * x.first - 6. * x.second;
	}

	double customMax(double** y, double** y_old, pair<int, int> size) {
		double maxValue = 0;
		for (int i = 1; i < size.first - 1; i++) {
			for (int j = 1; j < size.second - 1; j++) {
				double value = abs((y[i][j] - y_old[i][j]) / y[i][j]);
				if (value > maxValue)
					maxValue = value;
			}
		}
		return maxValue;
	}

	string customRound(double number, int n) {
		string str = to_string(number);
		string result = "";

		int counter;
		int afterDot = -1;
		for (int i = 0; i < str.length(); i++) {
			if (afterDot > n) break;
			if (afterDot >= 0) afterDot++;
			if (str[i] == '.') afterDot = 0;

			counter = i;
			result = str.substr(0, i);
		}
		return result;
	}

	void showEps()		{ cout << "epsilon:\t"		<< eps		<< endl; }

	void showPsi()		{ cout << "psi:\t\t"		<< psi		<< endl; }

	void showOmega()	{ cout << "omega:\t\t"		<< omega	<< endl; }

	void showK()		{ cout << "k:\t\t"			<< k		<< endl; }

	double findBestSolution(double omegaStep) {
		solve(omegaStep);
		int counter = k;
		double bestParameter = omegaStep;

		for (double i = 2*omegaStep; i < 2; i += omegaStep) {
			solve(i);
			if (k < counter) {
				counter = k;
				bestParameter = i;
			}
		}
		return bestParameter;
	}

	void show(bool grids) {
		if (grids) {
			showGrids("x");
			showGrids("y");
			showGrids("z");
		}
		showEps();
		showPsi();

		showOmega();
		showK();
	}
};

class AdvancedSolver : Solver {
public:
	AdvancedSolver(pair<double, double> interval, int N, double epsilon) : Solver(interval, N, epsilon) { }

	void solve() {
		// assign and fill 'y' grid
		this->y = new double* [gridSize];
		for (int i = 0; i < gridSize; i++)
			this->y[i] = new double[gridSize];
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				if (x[i][j].first == 0) {
					this->y[i][j] = InitialValues::mu1(x[i][j].second);
				}
				else if (x[i][j].first == interval.second) {
					this->y[i][j] = InitialValues::mu2(x[i][j].second);
				}
				else if (x[i][j].second == 0) {
					this->y[i][j] = InitialValues::mu3(x[i][j].first);
				}
				else if (x[i][j].second == interval.second) {
					this->y[i][j] = InitialValues::mu4(x[i][j].first);
				}
				else {
					this->y[i][j] = 0;
				}
			}
		}

		// assign and fill 'z' grid
		this->z = new double* [gridSize];
		for (int i = 0; i < gridSize; i++)
			this->z[i] = new double[gridSize];
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				z[i][j] = 0;
			}
		}
		this->psi = 0;
		this->k = 0;
		// create and copy 'y_old' grid from 'y' grid
		double** y_old = new double* [gridSize];
		for (int i = 0; i < gridSize; i++)
			y_old[i] = new double[gridSize];
		do {
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					y_old[i][j] = y[i][j];
				}
			}
			for (int i = 1; i < gridSize - 1; i++)
				for (int j = 1; j < gridSize - 1; j++)
					y[i][j] = 0.05 * (4.0 * (y[i - 1][j] + y[i][j - 1] + y_old[i + 1][j] + y_old[i][j + 1]) + 
						(y[i - 1][j - 1] + y[i - 1][j + 1] + y[i + 1][j - 1]+ y[i + 1][j + 1])) + 0.3 * h * h * f(x[i][j]);
			k++;
		} while (customMax(y, y_old, { gridSize, gridSize }) >= eps);
		// find 'psi' value
		for (int i = 1; i < gridSize - 1; i++) {
			for (int j = 1; j < gridSize - 1; j++) {
				z[i][j] = abs(y[i][j] - U(x[i][j]));
				if (z[i][j] > psi) psi = z[i][j];
			}
		}
	}

	void show(bool grids) {
		if (grids) {
			showGrids("x");
			showGrids("y");
			showGrids("z");
		}
		showEps();
		showPsi();

		showK();
	}

};

void main() {
	pair<double, double>	interval	= { 0.0, 1.0 };
	int						N			= 10;
	double					epsilon		= 1e-6;
	double					omegaStep   = 0.01;

	Solver* s1 = new Solver(interval, N, epsilon);

	// s1->solve(s1->findBestSolution(omegaStep));
	s1->solve(1.0);
	s1->show(false);

	cout << "\n";

	AdvancedSolver* as1 = new AdvancedSolver(interval, N, epsilon);
	as1->solve();
	as1->show(false);
	// as1->show();
}