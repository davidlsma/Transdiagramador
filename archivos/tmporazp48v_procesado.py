class Factura:
{
__numeroFactura: str
__fecha: object
__importe: float
__cuit: str
__denominacionNegocio: str
__descuento: float
def __init__(self, numeroFactura,fecha, importe, cuit, denominacionNegocio):
{
self.__numeroFactura=numeroFactura
self.__fecha=fecha
self.__importe=importe
self.__cuit=cuit
self.__denominacionNegocio=denominacionNegocio
}
def getTotal(self):
{
return self.__importe-self.__descuento
}
def getNumeroFactura(self):
{
return self.__numeroFactura
}
}
class Cliente:
{
__idCliente: int
__nombreyApellido: str
__direccion: str
__facturas: list[Factura]
def __init__(self,idCliente, nombreyApellido, direccion):
{
self.__idCliente=idCliente
self.__nombreyApellido=nombreyApellido
self.__direccion=direccion
}
def setFactura(self, unaFactura):
{
self.__facturas.append(unaFactura)
}
}
