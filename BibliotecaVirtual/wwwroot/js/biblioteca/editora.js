/** Funções para controle das informaçoes da entidade de Editora.
  * Fonte: js/biblioteca/editora.js
  */
'use strict';

function editora()
{
    if ($('#selectPublisher').length)
    {
        editora.vueSelect = new Vue({
            el: '#selectPublisher',
            data: {
                selectPublisher: [],
                isEmpty: this.selectPublisher.length == 0,
                selectedValue: -1,
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/publisher/getpublishers';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.length)
                            {
                                let data = response;
                                editora.vueSelect.atualizarLista(data);
                            }

                            $('#selectPublisher .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectPublisher = data;
                    this.$forceUpdate();
                },
                setSelectedValue: function (value)
                {
                    if (value && parseInt(value))
                    {
                        this.selectedValue = value;
                    }
                },
                getPublisherId: function (item)
                {
                    return item.publisherId;
                },
                getPublisherName: function (item)
                {
                    return item.name;
                },
                newPublisher: function (url)
                {
                    window.open(url, '_blank');
                },
            },
            created: function ()
            {
                this.obterOpcoes();
            }
        });
    }
}
editora();