import { ImageServicehttpClient } from "../../api"
import { postImagesURL } from "../config"
import { CreateImage } from "../model"
// export const getCategories = async () => {
//     const response = await ImageServicehttpClient.post(`${SLUG}/${postImagesURL}`);
//     return response.data as Category[];
// };
const SLUG = "Image"

 type CreateImages = {
    productId: string
    images: CreateImage[]
}

// export const uploadImages = async ({productId, images} : CreateImages) => {
//     const formData = new FormData();
//     formData.append('ProductId', productId);
  
//     images.forEach((image, index) => {
//       formData.append(`Images[${index}].Order`, image.order.toString());
//       formData.append(`Images[${index}].Image`, image.image);
//     });
  
//     try {
//       const response = await ImageServicehttpClient.post(`${SLUG}/${postImagesURL}`, formData, {
//         headers: {
//           'Content-Type': 'multipart/form-data',
//         },
//       });
//       console.log('Upload successful:', response.data);
//     } catch (error) {
//       console.error('Upload failed:', error);
//     }
//   };




export const uploadImages = async ({productId, images}: CreateImages): Promise<void> => {
  const formData = new FormData();
  formData.append('ProductId', productId);

  images.forEach((image, index) => {
    formData.append(`Images[${index}].Order`, image.order.toString());
    formData.append(`Images[${index}].Image`, image.image); // image.image должен быть File
  });

  try {
    const response = await ImageServicehttpClient.post(
      `${SLUG}/${postImagesURL}`, 
      formData
    );
    return response.data;
  } catch (error: any) {
    if (error.response) {
      const errorData = error.response.data;
      
      // Для формата ошибок ASP.NET Core
      if (errorData.errors) {
        const messages = Object.values(errorData.errors).flat();
        throw new Error(messages.join(', '));
      }
      
      throw new Error(errorData.title || errorData);
    }
    throw new Error('Network error');
  }
};