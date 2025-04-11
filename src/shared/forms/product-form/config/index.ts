import { Image } from "@/shared/services/image-service-product-images";

export type ProductForm = {
  images: Image[];
  title: string;
  description: string;
  price: number;
  categoryId: string;
  sellerId: string;
};
