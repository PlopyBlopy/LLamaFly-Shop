import { RegisterSellerForm } from "@/features/register-seller-form";
import { ProfileRegister, SellerRegister, UserRegister } from "@/shared/services/auth-service-user";
import { useStore } from "@/shared/hooks/store-hook";
import { useAppNavigate } from "@/shared/routing/routes";

export const RegisterSellerPage = () => {
  const {
    authStore: { registerSellerAction },
  } = useStore();
  const { goToMain } = useAppNavigate();

  const handleRegister = async (userRegister: UserRegister, profileRegister: ProfileRegister, sellerRegister: SellerRegister) => {
    await registerSellerAction(userRegister, profileRegister, sellerRegister);
    await goToMain();
  };

  return (
    <>
      <RegisterSellerForm onRegister={handleRegister} />
    </>
  );
};
