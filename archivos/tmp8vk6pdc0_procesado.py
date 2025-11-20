class Proyeccion:
{
__fecha: str
__hora: str
def __init__(self, fecha, hora):
{
self.__fecha=fecha
self.__hora=hora
}
}
class Sala:
{
__numeroSala: int
__aforo: int
__proyecciones: list[Proyeccion]
def __init__(self, numeroSala, aforo):
{
self.__numeroSala=numeroSala
self.__aforo=aforo
}
def mostrarSala(self):
{
pass
}
}
class Cine:
{
__codigo: str
__denominacion: str
__empresaPropietaria: str
__direccion: str
__provincia: str
__salas: list[Sala]
def __init__(self, codigo, denominacion, empresaPropietaria, direccion, provincia):
{
self.__codigo=codigo
self.__denominacion=denominacion
self.__empresaPropietaria=empresaPropietaria
self.__direccion=direccion
self.__provincia=provincia
self.__salas=[]
}
def agregarSala(self):
{
sala = Sala()
self.salas.append(sala)
}
def mostrarCine(self):
{
pass
}
}
class Pelicula:
{
__titulo: str
__genero: str
__duracion: int
__clasificacion: str
__proyecciones: list[Proyeccion]
def __init__(self, titulo, genero, duracion, clasificacion):
{
self.__titulo=titulo
self.__genero=genero
self.__duracion=duracion
self.__clasificacion=clasificacion
}
}
