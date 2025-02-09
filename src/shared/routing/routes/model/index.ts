import { useNavigate } from 'react-router-dom';
import { ROUTES } from '../';

export const useAppNavigate = () => {
  const navigate = useNavigate();
  
  return {
    goMain: () => navigate(ROUTES.MAIN.path),
    
    goToProduct: (title: string, id: string) => 
        navigate(ROUTES.PRODUCT.path(title, id)),

    goToProfile: (id: string) =>
        navigate(ROUTES.PROFILE.path(id)),

    goToAddProduct: () =>
        navigate(ROUTES.ADD_PRODUCT.path()),
    
    goToSettings: () =>
        navigate(ROUTES.SETTINGS.path()),
    
    customNavigate: navigate,
  };
};