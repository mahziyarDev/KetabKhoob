using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Domain.ProductAgg;

public class Product : AggregateRoot
{
    #region Properties

    public string Title { get; private set; }
    public string ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification> Specifications { get;private set; }

    #endregion

    #region Functions

    private Product()
    {
        
    }
    public Product(string title, string imageName, string description,
        long categoryId, long subCategoryId, long secondarySubCategoryId,
        string slug, SeoData seoData,IProductDomainService productDomainService)
    {
        Guard(title,imageName,description,slug,productDomainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }   

    public void Edit (string title, string imageName, string description,
        long categoryId, long subCategoryId, long secondarySubCategoryId,
        string slug, SeoData seoData,IProductDomainService productDomainService)
    {
        Guard(title,imageName,description,slug,productDomainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void AddImage(ProductImage productImage)
    {
        productImage.ProductId = Id;
        Images.Add(productImage);
    }
    public void RemoveImage(long id)
    {
        var image = Images.FirstOrDefault(x=>x.Id == id);
        if (image == null) return;
        Images.Remove(image);
    }

    public void SetSpecifications(List<ProductSpecification> specs)
    {
        specs.ForEach(x=>x.ProductId = Id);
        Specifications = specs;
    }

    private void Guard(string title, string imageName, string description ,string slug,IProductDomainService productDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(description,nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName,nameof(imageName));
        NullOrEmptyDomainDataException.CheckString(slug,nameof(slug));
        if(slug != Slug)
            if (productDomainService.SlugIsExist(slug.ToSlug()))
                throw new SlugIsDuplicateException();
            

        
    }
    #endregion
    

    
    
    
}