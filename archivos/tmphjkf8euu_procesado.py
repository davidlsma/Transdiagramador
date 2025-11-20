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
