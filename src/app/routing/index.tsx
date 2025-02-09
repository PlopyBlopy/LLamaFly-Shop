import { createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../widgets/layouts/main-layout";
import { ProductsMainPage } from "../../pages/products-main-page";
import { ProductDetailPage } from "../../pages/product-detail-page";
import { ProfilePage } from "../../pages/profile-page";
import { ROUTES } from "../../shared/routing/routes";
import { AddProductPage } from "../../pages/add-product-page";
import { SettingsPage } from "../../pages/settings-page";

export const Router = createBrowserRouter([
  {
    path: ROUTES.MAIN.base,
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <ProductsMainPage />,
      },
      {
        path: ROUTES.PRODUCT.base,
        element: <ProductDetailPage />,
      },
      {
        path: ROUTES.PROFILE.base,
        element: <ProfilePage />,
      },
      {
        path: ROUTES.ADD_PRODUCT.base,
        element: <AddProductPage />,
      },
      {
        path: ROUTES.SETTINGS.base,
        element: <SettingsPage />,
      },
    ],
  },
]);
