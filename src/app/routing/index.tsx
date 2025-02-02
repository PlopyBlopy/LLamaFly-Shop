import { createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../widgets/layouts/main-layout";
import { ProductsMainPage } from "../../pages/products-main-page/index";
import { ProductDetailPage } from "../../pages/product-detail-page/index";

export const Router = createBrowserRouter([
  {
    path: "/",
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <ProductsMainPage />,
      },
      {
        path: "product/:title",
        element: <ProductDetailPage />,
      },
      //   {
      //     path: "profile/:profileId",
      //     // element:
      //   },
      //   {
      //     path: "cart/:cartId",
      //     // element:
      //   },
    ],
  },
]);
