/**
 * MÓDULO DE FUNCIONES GENÉRICAS / UTILERÍAS
 * Proyecto: Prueba Técnica BAC
 */

const Utilidades = {

    // ---------------------------------------------------------------------
    // 1. CONFIGURACIÓN INICIAL Y NOTIFICACIONES FLOTANTES (TOASTER)
    // ---------------------------------------------------------------------

    /**
     * Configura las opciones globales de Toastr si la librería está cargada.
     */
    initToastr: function () {
        if (typeof toastr !== 'undefined') {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "preventDuplicates": true,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "8000",
                "extendedTimeOut": "2000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
        }
    },

    /**
     * Muestra una notificación emergente tipo Toastr.
     * @param {string} mensaje - Texto de la alerta.
     * @param {string} titulo - Título opcional.
     * @param {string} tipo - 'success', 'error', 'info', 'warning'.
     */
    mostrarNotificacion: function (mensaje, titulo = '', tipo = 'success') {
        if (typeof toastr === 'undefined') {
            alert(`${titulo ? titulo + ': ' : ''}${mensaje}`);
            return;
        }

        switch (tipo.toLowerCase()) {
            case 'success':
                toastr.success(mensaje, titulo || '¡Éxito!');
                break;
            case 'error':
                toastr.error(mensaje, titulo || 'Error');
                break;
            case 'warning':
                toastr.warning(mensaje, titulo || 'Advertencia');
                break;
            case 'info':
                toastr.info(mensaje, titulo || 'Información');
                break;
            default:
                toastr.info(mensaje, titulo);
                break;
        }
    },

    // ---------------------------------------------------------------------
    // 2. ALERTAS DE CONFIRMACIÓN Y MENSAJES (SWEETALERT2)
    // ---------------------------------------------------------------------

    /**
     * Muestra un cuadro de confirmación interactivo con SweetAlert2.
     * @param {string} titulo - Título de la alerta.
     * @param {string} mensaje - Descripción detallada.
     * @param {function} callbackConfirm - Función a ejecutar si el usuario presiona "Sí".
     * @param {string} textoConfirmar - Texto del botón de confirmación.
     */
    confirmarAccion: function (titulo, mensaje, callbackConfirm, textoConfirmar = 'Sí, continuar') {
        if (typeof Swal === 'undefined') {
            if (confirm(`${titulo}\n\n${mensaje}`)) {
                callbackConfirm();
            }
            return;
        }

        Swal.fire({
            title: titulo,
            text: mensaje,
            icon: 'warning',
            width: '500px', // <--- Agrega esto para controlar el ancho (puedes probar 380px o 400px)
            showCloseButton: true, // Mostrar X en la esquina superior derecha
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#6c757d',
            confirmButtonText: textoConfirmar,
            cancelButtonText: 'Cancelar',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                callbackConfirm();
            }
        });
    },

    /**
     * Muestra un mensaje SweetAlert2 de éxito o error tipo modal.
     * @param {string} titulo - Título de la alerta.
     * @param {string} mensaje - Descripción del mensaje.
     * @param {string} icono - 'success', 'error', 'warning', 'info'.
     */
    mostrarAlertaModal: function (titulo, mensaje, icono = 'success') {
        if (typeof Swal === 'undefined') {
            alert(`${titulo}: ${mensaje}`);
            return;
        }

        Swal.fire({
            title: titulo,
            text: mensaje,
            icon: icono,
            confirmButtonColor: '#0d6efd',
            confirmButtonText: 'Aceptar'
        });
    },

    // ---------------------------------------------------------------------
    // 3. PETICIONES HTTP GENÉRICAS (AJAX HELPER)
    // ---------------------------------------------------------------------

    /**
     * Envoltorio genérico para llamadas AJAX (POST, PUT, DELETE, GET).
     * @param {string} url - Endpoint del controlador MVC.
     * @param {string} metodo - 'GET', 'POST', 'PUT', 'DELETE'.
     * @param {object|null} data - Objeto de datos a enviar en el body.
     * @param {function} onSuccess - Callback si la petición es exitosa.
     * @param {string} mensajeExito - Mensaje opcional para Toastr al completar con éxito.
     */
    ejecutarAjax: function (url, metodo, data = null, onSuccess = null, mensajeExito = null) {
        let self = this;
        $.ajax({
            url: url,
            type: metodo,
            contentType: 'application/json; charset=utf-8',
            data: data ? JSON.stringify(data) : null,
            success: function (res) {
                if (mensajeExito) {
                    self.mostrarNotificacion(mensajeExito, '', 'success');
                }
                if (typeof onSuccess === 'function') {
                    onSuccess(res);
                }
            },
            error: function (xhr) {
                let errorMsg = 'Ocurrió un error al procesar la solicitud.';
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMsg = xhr.responseJSON.message;
                } else if (xhr.responseText) {
                    errorMsg = xhr.responseText;
                }
                self.mostrarNotificacion(errorMsg, 'Atención', 'error');
            }
        });
    }
};

// Inicializar la configuración de Toastr automáticamente al cargar el script
Utilidades.initToastr();