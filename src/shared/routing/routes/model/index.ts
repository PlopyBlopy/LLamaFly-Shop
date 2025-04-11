import { useNavigate } from "react-router-dom";
import { ROUTES } from "../";
import { Transliterate } from "../../../../features/transliter";

export const useAppNavigate = () => {
  const navigate = useNavigate();

  return {
    goToMain: () => navigate(ROUTES.MAIN.path),

    goToProduct: (title: string, id: string) =>
      navigate(ROUTES.PRODUCT.path(Transliterate(title), id)),

    goToProfile: (id: string) => navigate(ROUTES.PROFILE.path(id)),

    goToSellerProducts: () => navigate(ROUTES.SELLER_PRODUCTS.path()),

    goToAddProduct: () => navigate(ROUTES.ADD_PRODUCT.path()),

    goToSettings: () => navigate(ROUTES.SETTINGS.path()),

    goToLogin: () => navigate(ROUTES.LOGIN.path()),

    goToRegister: () => navigate(ROUTES.REGISTER.path()),

    goToRegisterSeller: () => navigate(ROUTES.REGISTER_SELLER.path()),

    goToRegisterCustomer: () => navigate(ROUTES.REGISTER_CUSTOMER.path()),

    customNavigate: navigate,
  };
};
