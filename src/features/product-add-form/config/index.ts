import { Image } from "../../../shared/image-service-images";

export type ProductForm = {
  images: Image[];
  title: string;
  description: string;
  price: number;
  categoryId: string;
  sellerId: string;
};
