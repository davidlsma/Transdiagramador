class Persona:
{
_apellido: str
__nombre: str
edad: int
def __init__(self, apellido, nombre, edad):
{
self._apellido = apellido
self.__nombre = nombre
self.edad = edad
}
def saludar(self):
{
return "Hola"
}
def get_edad(self):
{
return self.edad
}
}
class Empleado(Persona):
{
__salario: int
__departamento: object
def __init__(self, nombre, edad, salario):
{
super().__init__(nombre, edad)
self.__salario = salario
self.__departamento = None
}
def asignar_departamento(self, departamento):
{
self.__departamento = departamento
}
}
class Departamento:
{
__nombre: str
__empleados: list[Empleado]
def __init__(self, nombre):
{
self.__nombre = nombre
self.__empleados = []
}
def agregar_empleado(self, empleado):
{
self.__empleados.append(empleado)
empleado.asignar_departamento(self)
}
}
class Empresa:
{
__nombre = "Empresa.SRL"
__departamentos: list[Departamento]
def __init__(self, nombre):
{
self.__nombre = nombre
self.__departamentos = []
}
def agregar_departamento(self, departamento):
{
self.__departamentos.append(departamento)
}
}
