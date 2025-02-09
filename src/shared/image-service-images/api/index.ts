import { ImageServicehttpClient } from "../../api"
import { postImagesURL } from "../config"
import { CreateImage } from "../model"
// export const getCategories = async () => {
//     const response = await ImageServicehttpClient.post(`${SLUG}/${postImagesURL}`);
//     return response.data as Category[];
// };
const ENDPOINT = "Image"

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
      `${ENDPOINT}/${postImagesURL}`, 
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

export const getRandomImageFromUnsplash = async () => {
  const images = [
      "https://plus.unsplash.com/premium_photo-1664392147011-2a720f214e01?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D",
      "https://plus.unsplash.com/premium_photo-1679913792906-13ccc5c84d44?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D",
      "https://images.unsplash.com/photo-1556228578-8c89e6adf883?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D",
      "https://images.unsplash.com/photo-1503602642458-232111445657?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D",
      "https://images.unsplash.com/photo-1541643600914-78b084683601?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
      "https://plus.unsplash.com/premium_photo-1718913936342-eaafff98834b?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
      "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
      "https://images.unsplash.com/photo-1491637639811-60e2756cc1c7?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzB8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
      "https://images.unsplash.com/photo-1532667449560-72a95c8d381b?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDh8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
      "https://images.unsplash.com/photo-1567721913486-6585f069b332?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTZ8fHByb2R1Y3R8ZW58MHx8MHx8fDA%3D",
    ];
    const randomIndex = Math.floor(Math.random() * images.length);
    return images[randomIndex];
};