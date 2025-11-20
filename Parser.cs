using Transdiagramdorfinal.Semantica;
using System.Collections.Generic;




using System;



public class Parser {
	public const int _EOF = 0;
	public const int _identificador = 1;
	public const int _numero = 2;
	public const int _cadenaCh = 3;
	public const int _nuevaLinea = 4;
	public const int _int = 5;
	public const int _str = 6;
	public const int _object = 7;
	public const int _list = 8;
	public const int _float = 9;
	public const int _bool = 10;
	public const int _def = 11;
	public const int _self = 12;
	public const int _none = 13;
	public const int _class = 14;
	public const int _pass = 15;
	public const int _print = 16;
	public const int _return = 17;
	public const int _LBRACE = 18;
	public const int _RBRACE = 19;
	public const int maxT = 41;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

public TablaClases tabla = new TablaClases(); 



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void sintaxisPy() {
		while (la.kind == 14) {
			DeclaracionClase();
			if (la.kind == 4) {
				Get();
			}
		}
		Expect(0);
	}

	void DeclaracionClase() {
		Expect(14);
		Expect(1);
		string nombreClase = t.val; tabla.NuevaClase(nombreClase);
		if (la.kind == 20) {
			Get();
			ListaHerencia();
			Expect(21);
		}
		Expect(22);
		if (la.kind == 4) {
			Get();
		}
		Expect(18);
		while (la.kind == 4) {
			Get();
		}
		DefinicionAtributo();
		if (la.kind == 4) {
			Get();
		}
		while (la.kind == 1 || la.kind == 24 || la.kind == 25) {
			DefinicionAtributo();
			if (la.kind == 4) {
				Get();
			}
		}
		DefinicionConstructor();
		if (la.kind == 4) {
			Get();
		}
		if (la.kind == 11) {
			DefinicionMetodo();
			if (la.kind == 4) {
				Get();
			}
			while (la.kind == 11) {
				DefinicionMetodo();
				if (la.kind == 4) {
					Get();
				}
			}
		}
		Expect(19);
	}

	void ListaHerencia() {
		Expect(1);
		string claseBase = t.val; tabla.AgregarHerencia(claseBase); 
		while (la.kind == 23) {
			Get();
			Expect(1);
			string otraClase = t.val; tabla.AgregarHerencia(otraClase); 
		}
	}

	void DefinicionAtributo() {
		if (la.kind == 24 || la.kind == 25) {
			if (la.kind == 24) {
				Get();
			} else {
				Get();
			}
		}
		string visibilidad = t.val; 
		if (visibilidad == "__") visibilidad = "-";
		else if (visibilidad == "_") visibilidad = "#";
		else visibilidad = "+"; 
		Expect(1);
		string nombreAtr = t.val; 
		string tipo = "Desconocido"; string valorInicial = null; 
		if (la.kind == 22) {
			Get();
			Tipo(out tipo);
		}
		if (la.kind == 26) {
			Get();
			if (la.kind == 2) {
				Get();
				valorInicial = t.val; 
			} else if (la.kind == 3) {
				Get();
				valorInicial = t.val; 
			} else if (la.kind == 27) {
				Get();
				valorInicial = "[]"; 
			} else if (la.kind == 13) {
				Get();
				valorInicial = "None"; 
			} else SynErr(42);
		}
		tabla.AgregarAtributo(visibilidad, nombreAtr, tipo, valorInicial);
		List <string> primitivos = new List<string> {"int", "str", "float", "bool"};
		if (!primitivos.Contains(tipo)) 
		{
		 string relacion = "asociacion";
		 string tipoDestino = tipo;
		 string cardOrigen = "1";
		 string cardDestino = "1";
		 
		 if (tipo.StartsWith("list[")) 
		 {
		   tipoDestino = tipo.Substring(5, tipo.Length - 6);
		   cardDestino = "*";
		 }
		
		 Clase claseDestino = tabla.BuscarClase(tipoDestino);
		 if (claseDestino != null) 
		 {
		   tabla.AgregarRelacion(tabla.ClaseActual.Nombre, claseDestino.Nombre, relacion, cardOrigen, cardDestino);
		 }
		}
		
	}

	void DefinicionConstructor() {
		Expect(11);
		Expect(30);
		string constructor = t.val; List<string> parametros = new List<string>(); tabla.AgregarMetodo(constructor, parametros); 
		Expect(20);
		Expect(12);
		if (la.kind == 23) {
			Get();
			Expect(1);
			if (la.kind == 26) {
				Get();
				Term();
			}
			while (la.kind == 23) {
				Get();
				Expect(1);
				if (la.kind == 26) {
					Get();
					Term();
				}
			}
		}
		Expect(21);
		Expect(22);
		Expect(4);
		Expect(18);
		if (la.kind == 4) {
			Get();
		}
		if (la.kind == 31) {
			Get();
			Expect(20);
			if (la.kind == 1) {
				Get();
				while (la.kind == 23) {
					Get();
					Expect(1);
				}
			}
			Expect(21);
			Expect(4);
		}
		Sentencia();
		if (la.kind == 4) {
			Get();
		}
		while (StartOf(1)) {
			Sentencia();
			if (la.kind == 4) {
				Get();
			}
		}
		Expect(19);
	}

	void DefinicionMetodo() {
		Expect(11);
		Expect(1);
		string nombreFunc = t.val; 
		Expect(20);
		List<string> parametros = new List<string>(); 
		Expect(12);
		if (la.kind == 1 || la.kind == 23) {
			ListaParametros(out parametros);
		}
		Expect(21);
		Expect(22);
		Expect(4);
		tabla.AgregarMetodo(nombreFunc, parametros); 
		Expect(18);
		if (la.kind == 4) {
			Get();
		}
		Sentencia();
		if (la.kind == 4) {
			Get();
		}
		while (StartOf(1)) {
			Sentencia();
			if (la.kind == 4) {
				Get();
			}
		}
		Expect(19);
	}

	void Tipo(out string tipo) {
		tipo = "Desconocido"; 
		switch (la.kind) {
		case 5: {
			Get();
			tipo = "int"; 
			break;
		}
		case 6: {
			Get();
			tipo = "str"; 
			break;
		}
		case 7: {
			Get();
			tipo = "object"; 
			break;
		}
		case 8: {
			Get();
			tipo = "list"; 
			if (la.kind == 28) {
				Get();
				Expect(1);
				tipo = "list[" + t.val + "]"; 
				Expect(29);
			}
			break;
		}
		case 9: {
			Get();
			tipo = "float"; 
			break;
		}
		case 10: {
			Get();
			tipo = "bool"; 
			break;
		}
		case 1: {
			Get();
			tipo = t.val; 
			break;
		}
		default: SynErr(43); break;
		}
	}

	void Term() {
		switch (la.kind) {
		case 2: {
			Get();
			break;
		}
		case 3: {
			Get();
			break;
		}
		case 27: {
			Get();
			break;
		}
		case 13: {
			Get();
			break;
		}
		case 32: {
			Get();
			if (la.kind == 24 || la.kind == 25) {
				if (la.kind == 24) {
					Get();
				} else {
					Get();
				}
			}
			Expect(1);
			break;
		}
		case 1: {
			Get();
			if (la.kind == 20) {
				string nombreCls = t.val; 
				Get();
				if (la.kind == 1) {
					Get();
					while (la.kind == 23) {
						Get();
						Expect(1);
					}
				}
				Expect(21);
				tabla.AgregarRelacion(tabla.ClaseActual.Nombre, nombreCls, "composicion"); 
			}
			break;
		}
		default: SynErr(44); break;
		}
	}

	void Sentencia() {
		if (StartOf(2)) {
			if (la.kind == 32) {
				Get();
			}
			if (la.kind == 24 || la.kind == 25) {
				if (la.kind == 24) {
					Get();
				} else {
					Get();
				}
			}
			Expect(1);
			if (la.kind == 26) {
				Asignacion();
			} else if (la.kind == 33) {
				Llamada();
			} else SynErr(45);
		} else if (la.kind == 16) {
			Get();
			Expect(20);
			Expect(3);
			Expect(21);
		} else if (la.kind == 15) {
			Get();
		} else if (la.kind == 17) {
			Get();
			if (StartOf(3)) {
				Expresion();
			}
		} else SynErr(46);
	}

	void ListaParametros(out List<string> parametros ) {
		if (la.kind == 23) {
			Get();
		}
		Expect(1);
		string param = t.val; parametros = new List<string>(); parametros.Add(param); 
		while (la.kind == 23) {
			Get();
			Expect(1);
			string otroParam = t.val; parametros.Add(otroParam); 
		}
	}

	void Asignacion() {
		Expect(26);
		Expresion();
	}

	void Llamada() {
		Expect(33);
		Expect(1);
		Expect(20);
		if (la.kind == 1 || la.kind == 12) {
			if (la.kind == 12) {
				Get();
			} else {
				Get();
			}
			while (la.kind == 23) {
				Get();
				if (la.kind == 12) {
					Get();
				} else if (la.kind == 1) {
					Get();
				} else SynErr(47);
			}
		}
		Expect(21);
	}

	void Expresion() {
		Term();
		if (StartOf(4)) {
			switch (la.kind) {
			case 34: {
				Get();
				break;
			}
			case 35: {
				Get();
				break;
			}
			case 36: {
				Get();
				break;
			}
			case 37: {
				Get();
				break;
			}
			case 38: {
				Get();
				break;
			}
			case 39: {
				Get();
				break;
			}
			case 40: {
				Get();
				break;
			}
			}
			Term();
		}
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		sintaxisPy();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _x,_x,_x,_x, _T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "identificador expected"; break;
			case 2: s = "numero expected"; break;
			case 3: s = "cadenaCh expected"; break;
			case 4: s = "nuevaLinea expected"; break;
			case 5: s = "int expected"; break;
			case 6: s = "str expected"; break;
			case 7: s = "object expected"; break;
			case 8: s = "list expected"; break;
			case 9: s = "float expected"; break;
			case 10: s = "bool expected"; break;
			case 11: s = "def expected"; break;
			case 12: s = "self expected"; break;
			case 13: s = "none expected"; break;
			case 14: s = "class expected"; break;
			case 15: s = "pass expected"; break;
			case 16: s = "print expected"; break;
			case 17: s = "return expected"; break;
			case 18: s = "LBRACE expected"; break;
			case 19: s = "RBRACE expected"; break;
			case 20: s = "\"(\" expected"; break;
			case 21: s = "\")\" expected"; break;
			case 22: s = "\":\" expected"; break;
			case 23: s = "\",\" expected"; break;
			case 24: s = "\"_\" expected"; break;
			case 25: s = "\"__\" expected"; break;
			case 26: s = "\"=\" expected"; break;
			case 27: s = "\"[]\" expected"; break;
			case 28: s = "\"[\" expected"; break;
			case 29: s = "\"]\" expected"; break;
			case 30: s = "\"__init__\" expected"; break;
			case 31: s = "\"super().__init__\" expected"; break;
			case 32: s = "\"self.\" expected"; break;
			case 33: s = "\".\" expected"; break;
			case 34: s = "\"+\" expected"; break;
			case 35: s = "\"-\" expected"; break;
			case 36: s = "\">=\" expected"; break;
			case 37: s = "\"<=\" expected"; break;
			case 38: s = "\">\" expected"; break;
			case 39: s = "\"<\" expected"; break;
			case 40: s = "\"==\" expected"; break;
			case 41: s = "??? expected"; break;
			case 42: s = "invalid DefinicionAtributo"; break;
			case 43: s = "invalid Tipo"; break;
			case 44: s = "invalid Term"; break;
			case 45: s = "invalid Sentencia"; break;
			case 46: s = "invalid Sentencia"; break;
			case 47: s = "invalid Llamada"; break;

			default: s = "error " + n; break;
		}
        string message = $"Error de sintaxis en línea {line}, columna {col}: {s}";
        throw new Exception(message);
        count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
