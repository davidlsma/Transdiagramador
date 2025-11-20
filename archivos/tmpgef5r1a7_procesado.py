class Carrera:
{
__codigo: int
__nombre: str
__fecha_inicio = "13/3"
__duracion: str
__titulo: str
def __init__(self,cod,nombre,duracion,titulo):
{
self.__codigo= cod
self.__nombre= nombre
self.__duracion= duracion
self.__titulo= titulo  
}
def getCodigo(self):
{
return self.__codigo
}
def getNombre(self):
{
return self.__nombre
}
def getDuracion(self):
{
return self.__duracion
}
def getTitulo(self):
{
return self.__titulo
}
}
class Facultad:
{
__codigo: int
__nombre: str
__direccion: str
__localidad: str
__telefono: str
__carreras: None
def __init__(self,cod,nombre,direc,localidad,tel):
{
self.__codigo= cod
self.__nombre= nombre
self.__direccion= direc
self.__localidad= localidad
self.__telefono= tel
self.__carreras= Carrera()
}
def agregarCarrera(self,una_carrera):
{
self.__carreras.agregarCarrera(una_carrera)
}
def mostrarCarreras(self):
{
self.__carreras.mostrarNombreyDuracion()
}
def buscar(self,carrera):
{
pass
}
def getLen(self):
{
pass    
}
def getCodigo(self):
{
return self.__codigo
}
def getNombre(self):
{
return self.__nombre
}
def getDireccion(self):
{
return self.__direccion
}
def getLocalidad(self):
{
return self.__localidad
}
def getTelefono(self):
{
return self.__telefono
}
}
