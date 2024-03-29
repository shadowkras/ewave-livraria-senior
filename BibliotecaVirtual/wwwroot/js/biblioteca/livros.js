﻿/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/livros.js
  */
'use strict';

function livros()
{
    if ($('#gradeLivros').length)
    {
        livros.vueSelect = new Vue({
            el: '#gradeLivros',
            data: {
                gradeLivros: [],
                filtroPesquisa: '',
            },
            methods: {
                obterLivros: function ()
                {
                    var endereco = site.url + 'biblioteca/book/getbooks';
                    let parametros = {
                        filter: this.filtroPesquisa,
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.sucesso)
                            {
                                livros.vueSelect.atualizarLista(response.dados);
                            }
                            else
                            {
                                livros.vueSelect.atualizarLista([]);
                            }

                            $('#selectAuthor .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.gradeLivros = data;
                    this.$forceUpdate();
                    setTimeout(function ()
                    {
                        tooltips.atualizar('#gradeLivros [data-original-title]');
                    }, 1000);
                },
                isEmpty: function ()
                {
                    return this.gradeLivros.length == 0;
                },
                registroConsultar: function (url, item)
                {
                    return url + '?bookId=' + item.bookId;
                },
                getPublishDate: function (item)
                {
                    if (item && item.publishDate)
                    {
                        var date = new Date(item.publishDate.split('T')[0]);
                        return date.toLocaleDateString();
                    }
                    else
                    {
                        return '';
                    }
                },
                registroAlugar: function (item) {
                    var endereco = site.url + 'book/rent';
                    let parametros = {
                        bookId: item.bookId,
                    };

                    $.post(endereco, parametros)
                        .done(function (response) {
                            if (response && response.sucesso) {
                                alert('Livro alugado com sucesso');
                            }
                            else {
                                alert(response.mensagem);
                            }
                        });
                },
                filtroChanged: function ()
                {
                    this.obterLivros();
                },
            },
            created: function ()
            {
                this.obterLivros();
            },
        });
    }
}
livros();