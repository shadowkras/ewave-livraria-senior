/** Objeto que cria as as tooltips na aplicação.
  * Fonte: js/biblioteca/tooltips.js
 */
function tooltips()
{
    /** Cria as tooltips em todos os componentes com a a classe Jquery informada.
     * @param {string} jquery jQuery para encontrar o componente html. (Exemplo: '.tree')
     */
    tooltips.atualizar = function atualizar(jquery)
    {
        var tooltipOptions = {
            delay:
            {
                show: 500,
                hide: 200
            },
        };
        $(jquery).not('[rel*=popover]').tooltip(tooltipOptions);
    }

    $(document).ready(function ()
    {
        /** Cria as tooltips em todos os componentes com a propriedade 'data-original-title'. */
        tooltips.atualizar('[data-original-title]');
    });
}
tooltips();