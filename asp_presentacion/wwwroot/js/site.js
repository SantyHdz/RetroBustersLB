// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", function () {
    // 🔹 Obtener los elementos
    const cantidad = document.querySelector("input[name='Actual.Cantidad']");
    const precio = document.querySelector("input[name='Actual.Precio_unitario']");
    const guardarBtn = document.querySelector("input[type=submit][value='Guardar']");
    const totalDiv = document.getElementById("total-din");

    // 🔹 Mostrar total dinámico
    const mostrarTotal = () => {
        const cant = parseInt(cantidad?.value || 0);
        const prec = parseFloat(precio?.value || 0);
        if (totalDiv) {
            const total = cant * prec;
            totalDiv.textContent = `Total estimado: $${total.toFixed(2)}`;
        }
    };

    // 🔹 Habilitar/deshabilitar botón "Guardar" según validez de campos
    const validarFormulario = () => {
        if (!cantidad || !precio || !guardarBtn) return;
        const cantVal = cantidad.value.trim();
        const precVal = precio.value.trim();
        guardarBtn.disabled = !(cantVal !== "" && precVal !== "" && parseFloat(precVal) >= 0);
    };

    // 🔹 Resaltar campos modificados
    const aplicarResaltado = (input) => {
        input.classList.add("highlight-change");
        setTimeout(() => input.classList.remove("highlight-change"), 1500);
    };

    // 🔹 Asignar eventos
    [cantidad, precio].forEach(input => {
        if (!input) return;
        input.addEventListener("input", () => {
            mostrarTotal();
            validarFormulario();
            aplicarResaltado(input);
        });
    });

    // 🔹 Inicial
    mostrarTotal();
    validarFormulario();
});

