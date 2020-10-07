// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var site = {};
site.url = location.protocol + "//" + location.host + "/";

/** Congela o objeto para prevenir manipulação. */
Object.freeze(site);
/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/autor.js
  */
'use strict';

function autor()
{
    if ($('#selectAuthor').length)
    {
        autor.vueSelect = new Vue({
            el: '#selectAuthor',
            data: {
                selectAuthor: [],
                isEmpty: this.selectPublisher.length == 0,
                selectedValue: -1,
                loadedValue: -1,
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/author/getauthors';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.sucesso)
                            {
                                let data = response.dados;
                                autor.vueSelect.atualizarLista(data);
                            }
                            else {
                                autor.vueSelect.atualizarLista([]);
                            }

                            $('#selectAuthor .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectAuthor = data;
                    this.selectedValue = this.loadedValue;
                    this.$forceUpdate();
                },
                setSelectedValue: function (value)
                {
                    if (value && parseInt(value))
                    {
                        
                        this.loadedValue = value;
                    }
                },
                getAuthorId: function (item)
                {
                    return item.authorId;
                },
                getAuthorName: function (item)
                {
                    return item.name;
                },
                newAuthor: function (url)
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
autor();
/** Funções para controle das informaçoes da entidade de Autor(a).
  * Fonte: js/biblioteca/categoria.js
  */
'use strict';

function categoria()
{
    if ($('#selectCategory').length)
    {
        categoria.vueSelect = new Vue({
            el: '#selectCategory',
            data: {
                selectCategory: [],
                selectedValues: [],
                loadedValue: [],
            },
            methods: {
                obterOpcoes: function ()
                {
                    var endereco = site.url + 'biblioteca/category/getcategories';
                    let parametros = {
                    };

                    $.get(endereco, parametros)
                        .done(function (response)
                        {
                            if (response && response.sucesso)
                            {
                                let data = response.dados;
                                categoria.vueSelect.atualizarLista(data);
                            }
                            else {
                                categoria.vueSelect.atualizarLista([]);
                            }

                            $('#selectCategory .carregando').addClass('hidden');
                        });
                },
                atualizarLista: function (data)
                {
                    this.selectCategory = data;
                    this.selectedValues = this.loadedValue;
                    this.$forceUpdate();
                },
                isEmpty: function ()
                {
                    return this.selectCategory.length == 0 || false;
                },
                setSelectedValues: function (value)
                {
                    if (value)
                    {
                        if (typeof value === 'string' || value instanceof String)
                        {
                            if (value.search(',') !== -1)
                            {
                                var values = value.split(',');
                                this.loadedValue = values;
                            }
                            else
                            {
                                var values = [value];
                                this.loadedValue = values;
                            }
                        }
                    }
                },
                getCategoryId: function (item)
                {
                    return item.categoryId;
                },
                getCategoryName: function (item)
                {
                    return item.description;
                },
                newCategory: function (url)
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
categoria();
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
                            if (response && response.sucesso)
                            {
                                let data = response.dados;
                                editora.vueSelect.atualizarLista(data);
                            }
                            else {
                                editora.vueSelect.atualizarLista([]);
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
/* Fonte: js/biblioteca/image-dropzone-input.js */
$(document).ready(function ()
{
    // Declarações de variaveis que poderão ser reutilizadas no decorrer do script.

    let textoImagemTitulo = "";

    // Declarações de métodos de inicialização do componente.

    if ($('.img-output').length)
    {
        $('.img-output').each(function (i, elemento)
        {
            let dropzone = $(this).parent().find('.imagem-dropzone');
            if (dropzone.length)
            {
                let valorOutput = dropzone.parent().find('.img-output').val();

                if (valorOutput.length)
                {
                    $img = $('<img />').attr('src', valorOutput).fadeIn();
                    dropzone.find('.imagem-dropzone-texto').hide();
                    dropzone.find('.imagem-dropzone-src').html($img);
                    dropzone.find(".imagem-dropzone-btn-remover").show();
                    textoImagemTitulo = "Imagem selecionada"
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                } else
                {
                    textoImagemTitulo = "Nenhuma imagem selecionada"
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                }
            }
        });
    };

    // Declarações de métodos acionados por meio de clicks.
    $('.imagem-dropzone .imagem-dropzone-btn-remover').click(function (e)
    {
        let dropzone = $(this).parents('.imagem-dropzone');
        if (dropzone.length)
        {
            dropzone.find('.imagem-dropzone-texto').show();
            dropzone.find('.imagem-dropzone-src img').removeAttr("src").attr("src", "");
            dropzone.parent().find('.img-output').attr("value", "");
            dropzone.find('.imagem-dropzone-borda input[type="file"]').val(null);
            dropzone.find(".imagem-dropzone-btn-remover").hide();
            textoImagemTitulo = "Nenhuma imagem selecionada"
            dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
        }

    });

    // Declaração de métodos acionados por meio de drags over/leave e changes.

    $('.imagem-dropzone .imagem-dropzone-borda').on('dragover', function ()
    {
        $(this).addClass('hover');
    });

    $('.imagem-dropzone .imagem-dropzone-borda').on('dragleave', function ()
    {
        $(this).removeClass('hover');
    });


    $('.imagem-dropzone input[type="file"]').on('change', function (e)
    {
        let dropzone = $(this).parents('.imagem-dropzone');

        if (dropzone.length)
        {
            if (this.files.length > 0)
            {
                var file = this.files[0];

                if (this.accept && $.inArray(file.type, this.accept.split(/, ?/)) === -1) 
                {
                    smartBox.Aviso(null, 'Cancelado!', 'Verifique o formato do arquivo e tente novamente.', 6000);
                    dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                    dropzone.find('.imagem-dropzone-borda').removeClass('hover');

                    return;
                }

                dropzone.find('.imagem-dropzone-borda').removeClass('hover');
                dropzone.find('.imagem-dropzone-texto').hide();

                dropzone.find('.imagem-dropzone-borda').addClass('dropped');

                if (dropzone.find('.imagem-dropzone-src img').length)
                {
                    dropzone.find('.imagem-dropzone-src').removeAttr('img');
                }

                if ((/^image\/(jpg|png|jpeg)$/i).test(file.type)) 
                {
                    var reader = new FileReader(file);
                    reader.readAsDataURL(file);

                    reader.onload = function (e)
                    {
                        var data = e.target.result,
                            $img = $('<img />').attr('src', data).fadeIn();

                        dropzone.find('.imagem-dropzone-src').html($img);

                        var dataUrl = data.split(',')[1];
                        var imgOutput = dropzone.parent().find('.img-output');
                        imgOutput.attr("value", dataUrl).valid();

                        dropzone.find(".imagem-dropzone-btn-remover").show();
                        textoImagemTitulo = "Imagem selecionada"
                        dropzone.find('.imagem-dropzone-borda input[type="file"]').attr('title', textoImagemTitulo);
                    };
                }
                else
                {
                    var ext = file.name.split('.').pop();
                    dropzone.find('.imagem-dropzone-src').html(ext);
                }
            }
        }
    });
});
/** Funções para controle das informaçoes da entidade de Autor(a).
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