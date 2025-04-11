import { AxiosInstance } from "axios";
import { ImagesUpload } from "../model";
import JSZip from "jszip";
import { API } from "@/shared/api/http-client";

export const uploadProductImages = async (api: AxiosInstance, { productId, images }: ImagesUpload) => {
  const formData = new FormData();
  formData.append("ProductId", productId);

  images.forEach((image, index) => {
    formData.append(`Images[${index}].Order`, image.order.toString());
    formData.append(`Images[${index}].Image`, image.image);
  });

  try {
    await api.post(API.images.products.upload(), formData);
  } catch (error: any) {
    if (error.response) {
      const errorData = error.response.data;

      if (errorData.errors) {
        const messages = Object.values(errorData.errors).flat();
        throw new Error(messages.join(", "));
      }

      throw new Error(errorData.title || errorData);
    }
    throw new Error("Network error");
  }
};

export const getProductPreviews = async (api: AxiosInstance, productIds: string[]): Promise<Map<string, string>> => {
  try {
    const response = await api.post(API.images.products.previews(), { ProductIds: productIds }, { responseType: "blob" });

    const zip = new JSZip();
    const loadedZip = await zip.loadAsync(response.data);
    const images = new Map<string, string>();

    await Promise.all(
      Object.keys(loadedZip.files).map(async (fileName) => {
        const file = loadedZip.file(fileName);
        if (!file || file.dir) return;

        const productId = fileName.split("/")[0];
        const blob = await file.async("blob");
        images.set(productId, URL.createObjectURL(blob));
      })
    );

    return images;
  } catch (error) {
    return new Map();
  }
};

export const getProductImages = async (api: AxiosInstance, productId: string): Promise<Map<number, string>> => {
  try {
    const response = await api.get(API.images.products.detail(productId), {
      responseType: "blob",
    });

    const zip = new JSZip();
    const loadedZip = await zip.loadAsync(response.data);
    const images = new Map<number, string>();

    await Promise.all(
      Object.keys(loadedZip.files).map(async (fileName) => {
        const file = loadedZip.file(fileName);
        if (!file || file.dir) return;

        const order = fileName.split(".")[0]; //split не / а .
        const blob = await file.async("blob");
        images.set(Number(order), URL.createObjectURL(blob)); //явное приобразование к числу
      })
    );

    return images;
  } catch (error) {
    return new Map();
  }
};
