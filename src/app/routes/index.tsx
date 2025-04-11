import { createBrowserRouter } from "react-router-dom";
import { MainLayout } from "../../widgets/layouts/main-layout";
import { MainPage } from "../../pages/main-page";
import { ProductDetailPage } from "../../pages/product-detail-page";
import { ProfilePage } from "../../pages/profile-page";
import { ROUTES } from "../../shared/routing/routes";
import { AddProductPage } from "../../pages/product-add-page";
import { SettingsPage } from "../../pages/settings-page";
import { RegisterPage } from "../../pages/register-page";
import { LoginPage } from "../../pages/login-page";
import { RegisterSellerPage } from "../../pages/register-seller-page/ui";
import { RegisterCustomerPage } from "../../pages/register-customer-page/ui";
import { ProductsSellerPage } from "../../pages/products-seller-page/ui";

export const Router = createBrowserRouter([
  {
    path: ROUTES.MAIN.base,
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <MainPage />,
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
        path: ROUTES.SELLER_PRODUCTS.base,
        element: <ProductsSellerPage />,
      },
      {
        path: ROUTES.ADD_PRODUCT.base,
        element: <AddProductPage />,
      },
      {
        path: ROUTES.SETTINGS.base,
        element: <SettingsPage />,
      },
      {
        path: ROUTES.LOGIN.base,
        element: <LoginPage />,
      },
      {
        path: ROUTES.REGISTER.base,
        element: <RegisterPage />,
      },
      {
        path: ROUTES.REGISTER_SELLER.base,
        element: <RegisterSellerPage />,
      },
      {
        path: ROUTES.REGISTER_CUSTOMER.base,
        element: <RegisterCustomerPage />,
      },
    ],
  },
]);
