import { action, makeObservable } from "mobx";
import { AxiosInstance } from "axios";
import { getProductImages, getProductPreviews, ImagesUpload, uploadProductImages } from "@/shared/services/image-service-product-images";

// @injectable()
export default class ProductImageStore {
  private readonly api: AxiosInstance;
  // private imageCache = new Map<string, string>();
  // private imageCache2 = new Map<number, string>();

  constructor(api: AxiosInstance) {
    makeObservable(this, {
      addProductImagesAction: action,
      getProductsCardsPreviewImagesAction: action,
      getProductImagesAction: action,
    });

    this.api = api;
  }

  addProductImagesAction = async (images: ImagesUpload) => {
    await uploadProductImages(this.api, images);
  };

  getProductsCardsPreviewImagesAction = async (productIds: string[]) => {
    try {
      const images = await getProductPreviews(this.api, productIds);

      // runInAction(() => {
      //   images.forEach((url, productId) => {
      //     this.imageCache.set(productId, url);
      //   });
      // });

      return images;
    } catch (error) {
      console.error("Image loading failed:", error);
      return new Map();
    }
  };

  // getImage(productId: string) {
  //   return this.imageCache.get(productId) || "";
  // }

  // clearCache() {
  //   this.imageCache.forEach((url) => URL.revokeObjectURL(url));
  //   this.imageCache.clear();
  // }

  getProductImagesAction = async (productId: string) => {
    try {
      const images = await getProductImages(this.api, productId);

      // runInAction(() => {
      //   images.forEach((url, productId) => {
      //     this.imageCache2.set(productId, url);
      //   });
      // });

      return images;
    } catch (error) {
      console.error("Image loading failed:", error);
      return new Map();
    }
  };

  // getProductsCardsImagesAction = async (
  //   productIds: ProductIds
  // ): Promise<Map<string, string>> => {
  //   const data = new Map<string, string>();

  //   try {
  //     const response = await getProductPreviews(this.api, productIds);

  //     data = response;

  //     return data;
  //   } catch (error) {
  //     console.error("Image loading failed:", error);
  //     return data;
  //   }
  // };
  // getProductsCardsImagesAction = async (products: ProductCard[]) => {
  //   try {
  //     await Promise.all(
  //       products.map(async (product) => {
  //         try {
  //           const imageUrl = await getRandomImageFromUnsplash();
  //           runInAction(() => {
  //             product.image = imageUrl; // Обновляем поле image
  //           });
  //         } catch (error) {
  //           console.error(`Error loading image for ${product.id}`, error);
  //           runInAction(() => {
  //             product.image = "/default-image.jpg"; // Fallback
  //           });
  //         }
  //       })
  //     );
  //   } catch (error) {
  //     console.error("Image loading failed:", error);
  //   }
  // };

  // getProductImagesAction = async (productId: string): Promise<string[]> => {
  //   try {
  //     // const imageUrl = await getRandomImageFromUnsplash();
  //     let images: string[] = [];
  //     //TODO: Ускорить загрузку картинок (В том числе изменение в api)
  //     //TODO: загрузить все картинки сразу в zip файле или по одной но с учетом их количества

  //     for (let i = 0; i < 4; i++) {
  //       try {
  //         images.push(await getProductImageByOrder(this.api, productId, i));
  //       } catch {
  //         images.push("../../../../../dist/assets/noImage.webp");
  //       }
  //     }

  //     // const imageUrl = await getProductImage(this.api, productId);
  //     return images;
  //     // runInAction(() => {
  //     //   product.image = imageUrl; // Обновляем поле image
  //     // });
  //   } catch (error) {
  //     return ["/default-image.jpg"];
  //     // runInAction(() => {
  //     //   product.image = "/default-image.jpg"; // Fallback
  //     // });
  //   }
  // };
}
