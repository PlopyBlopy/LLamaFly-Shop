import { Image } from "@/shared/services/image-service-product-images";
import { Category } from "../../../shared/services/product-service-categories";

export type ProductFormProps = {
  onSubmit: (formData: ProductForm) => void;
  categories: Category[];
  seller: SellerProp;
};

export type SellerProp = {
  id: string;
  name: string;
  surname: string;
  patronymic: string;
};

export type ProductForm = {
  images: Image[];
  title: string;
  description: string;
  price: number;
  categoryId: string;
  sellerId: string;
};
