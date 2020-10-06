using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.ViewComponents
{
    [ViewComponent(Name = "ImagemDropzone")]

    public class ImagemDropzoneViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string extensoesImagensAceitas)
        {
            var imagemViewModel = new ImagemDropzoneViewModel(extensoesImagensAceitas);
            return View("ImagemDropzoneInput", imagemViewModel);
        }
    }

    public class ImagemDropzoneViewModel
    {
        public string ExtensoesImagensAceitas { get; }

        public ImagemDropzoneViewModel(string extensoesImagensAceitas)
        {
            if ((string.IsNullOrEmpty(extensoesImagensAceitas) == true) ||
                (extensoesImagensAceitas.Length == 1 &&
               extensoesImagensAceitas.Contains("*") == true))
            {
                ExtensoesImagensAceitas = "image/jpg, image/png, image/jpeg";
            }
            else if (string.IsNullOrEmpty(extensoesImagensAceitas) == false &&
                     extensoesImagensAceitas.Length > 1)
            {
                ExtensoesImagensAceitas = extensoesImagensAceitas;
            }
        }
    }
}
