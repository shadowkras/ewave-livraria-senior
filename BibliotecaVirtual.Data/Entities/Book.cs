using System;
using System.Collections.Generic;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;

namespace BibliotecaVirtual.Data.Entities
{
    /// <summary>
    /// Entidade de livro.
    /// </summary>
    public class Book : IDatabaseEntity
    {
        /// <summary>
        /// Identificador do livro.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Título do livro.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Descrição breve do livro.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sinopse do livro.
        /// </summary>
        public string Sinopsis { get; set; }

        /// <summary>
        /// Quantidade de páginas do livro.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Data de publicação do livro.
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Imagem da capa do livro.
        /// </summary>
        public byte[] CoverImage { get; set; }

        /// <summary>
        /// Identificador do autor do livro.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Identificador da editora do livro.
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Lista de categorias do livro.
        /// </summary>
        public ICollection<BookCategory> Categories { get; set; }

        /// <summary>
        /// Autor do livro.
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Editora do livro.
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Construtor da entidade Book, necessário para o EntityFramework.
        /// </summary>
        public Book()
        {
            Categories = new HashSet<BookCategory>();
        }

        /// <summary>
        /// Construtor com as informações requeridas de um livro.
        /// </summary>
        /// <param name="title">Título do livro.</param>
        /// <param name="description">Descrição breve do livro.</param>
        /// <param name="sinopsis">Sinopse do livro.</param>
        /// <param name="authorId">Identificador do autor do livro.</param>
        /// <param name="publisherId">Identificador da editora do livro.</param>
        /// <param name="publishDate">Data de publicação do livro.</param>
        /// <param name="pages">Quantidade de páginas do livro.</param>
        public Book(string title, string description, string sinopsis, int authorId, int publisherId, DateTime publishDate, int pages)
        {
            Title = title;
            Description = description;
            Sinopsis = sinopsis;
            AuthorId = authorId;
            PublisherId = publisherId;
            PublishDate = publishDate;
            Pages = pages;
        }

        /// <summary>
        /// Define a imagem de capa do livro.
        /// </summary>
        /// <param name="coverImage">Imagem da capa do livro.</param>
        public void SetCoverImage(byte[] coverImage)
        {
            if(coverImage.Length > 0)
                CoverImage = CoverImage;
        }

        /// <summary>
        /// Define a imagem de capa do livro.
        /// </summary>
        /// <param name="coverImage">Imagem da capa do livro.</param>
        public void SetCoverImage(string coverImage)
        {
            if (string.IsNullOrEmpty(coverImage) == false)
                CoverImage = coverImage.AsStringBase64();
        }

        /// <summary>
        /// Adiciona um novo gênero ao livro.
        /// </summary>
        /// <param name="category">Entidade de gênero.</param>
        public void SetCategory(Category category)
        {
            var bookCategory = new BookCategory(category.CategoryId, BookId);
            Categories.Add(bookCategory);
        }
    }
}
