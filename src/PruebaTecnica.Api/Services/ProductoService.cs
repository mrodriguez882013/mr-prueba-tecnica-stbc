using PruebaTecnica.Api.Dtos.Productos.Requests;
using PruebaTecnica.Api.Dtos.Productos.Responses;
using PruebaTecnica.Api.Entities;
using PruebaTecnica.Api.Exceptions;
using PruebaTecnica.Api.Repositories.Interfaces;
using PruebaTecnica.Api.Services.Interfaces;

namespace PruebaTecnica.Api.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProductoService(IProductoRepository productoRepository, IUsuarioRepository usuarioRepository)
        {
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
        }

        //------------------
        // Lecturas
        //------------------
        public async Task<IEnumerable<ProductoResponse>> ObtenerProductosAsync(
            string? codigo,
            CancellationToken cancellationToken = default)
        {
            var productos = await _productoRepository.ObtenerProductosAsync(codigo, cancellationToken);

            return productos.Select(p => new ProductoResponse
            {
                IdPro = p.IdPro,
                Codigo = p.Codigo,
                Producto = p.ProductoNombre,
                Precio = p.Precio,
                Stock = p.Stock,
                IdUsuarioCreacion = p.IdUsuarioCreacion,
                FechaCreacion = p.FechaCreacion
            });
        }

        //-----------------
        // Escrituras
        //-----------------

        public async Task CrearProductoAsync(
            ProductoCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            // Validaciones de negocio
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(request.IdUsuarioCreacion, cancellationToken)
                ?? throw new NotFoundException("El usuario especificado para crear el producto no existe.");

            var existeCodigo = (await _productoRepository.ObtenerProductosAsync(request.Codigo, cancellationToken))
                .Any(p => p.Codigo == request.Codigo);

            if (existeCodigo)
            {
                throw new ValidationException($"Ya existe un producto registrado con el código '{request.Codigo}'.");
            }

            var productoEntity = new Producto
            {
                Codigo = request.Codigo,
                ProductoNombre = request.Producto,
                Precio = request.Precio,
                Stock = request.Stock,
                IdUsuarioCreacion = request.IdUsuarioCreacion,
                FechaCreacion = DateTime.Now
            };

            await _productoRepository.InsertarProductoAsync(productoEntity);
        }

        public async Task ActualizarProductoAsync(
            int id, 
            ProductoUpdateRequest request,
            CancellationToken cancellationToken = default)
        {
            var productos = await _productoRepository.ObtenerProductosAsync(null, cancellationToken);
            var productoExistente = productos.FirstOrDefault(p => p.IdPro == id);

            if (productoExistente == null)
            {
                throw new NotFoundException($"No se encontró el producto con el ID {id}.");
            }

            var productoEntity = new Producto
            {
                Codigo = request.Codigo,
                ProductoNombre = request.Producto,
                Precio = request.Precio,
                Stock = request.Stock
            };

            await _productoRepository.ActualizarProductoAsync(id, productoEntity, cancellationToken);
        }

        public async Task EliminarProductoAsync(
            int id, 
            CancellationToken cancellationToken = default)
        {
            var productos = await _productoRepository.ObtenerProductosAsync(null, cancellationToken);
            var existe = productos.Any(p => p.IdPro == id);

            if (!existe)
            {
                throw new NotFoundException($"No se encontró el producto con el ID {id}.");
            }

            await _productoRepository.EliminarProductoAsync(id, cancellationToken);
        }
    }
}