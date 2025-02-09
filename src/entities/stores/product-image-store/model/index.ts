import { makeAutoObservable, runInAction } from "mobx";
import { getRandomImageFromUnsplash } from "../../../../shared/image-service-images";
import { Product, ProductCard } from "../../../../shared/product-service-products";

export default class ProductImageStore
{
    constructor()
    {
        makeAutoObservable(this);
    }
    public getProductsImagesAction = async (products: ProductCard[]) => {
        try {
            await Promise.all(
                products.map(async (product) => {
                    try {
                        const imageUrl = await getRandomImageFromUnsplash();
                        runInAction(() => {
                            product.image = imageUrl; // Обновляем поле image
                        });
                    } catch (error) {
                        console.error(`Error loading image for ${product.id}`, error);
                        runInAction(() => {
                            product.image = "/default-image.jpg"; // Fallback
                        });
                    }
                })
            );
        } catch (error) {
            console.error("Image loading failed:", error);
        }
    };

    public getProductImageAction = async (product: Product) => {
        try {
            const imageUrl = await getRandomImageFromUnsplash();
            
            runInAction(() => {
                product.image = imageUrl; // Обновляем поле image
            });

        } catch (error) {
            console.error(`Error loading image for ${product.id}`, error);
            runInAction(() => {
                product.image = "/default-image.jpg"; // Fallback
            });
        }
    };

}