import { ProductServicehttpClient } from "../../api/index.ts";
import { getProductCardsURL } from "../config/index.ts";
import { Product, ProductCard, QueryParams } from "../index.ts";

const SLUG = 'Products'

// export const getProductCards = async (params: QueryParams) =>
//     httpClient.get(`${SLUG}/${getProductCardsURL}`, {params}).then(response => response.data as ProductCard[]);

export const getProductCards = async (params: QueryParams) => {
    const response = await ProductServicehttpClient.get(`${SLUG}/${getProductCardsURL}`, { params });
    return response.data as ProductCard[];
};


export const getProductById = async (id: string) => {
    const response = await ProductServicehttpClient.get(`${SLUG}`, {
        params: {id},
    });
    return response.data as Product;
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

// export const getProducts = async (params: QueryParams) => {
//     const response = await httpClient.get(SLUG, { params });
//     return response.data as Product[];
// };

// export const getTodoById = (id: string) => 
//     httpClient.get(`${SLUG}/${id}`).then(response => response.data as Todo);

// export const updateTodo = (todo: Todo) => 
//     httpClient.put(`${SLUG}/${todo.id}`, {json: todo}).then(response => response.data as Todo)