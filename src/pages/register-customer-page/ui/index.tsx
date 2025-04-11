import { RegisterCustomerForm } from "@/features/register-customer-form";
import { CustomerRegister, ProfileRegister, UserRegister } from "@/shared/services/auth-service-user";
import { useStore } from "@/shared/hooks/store-hook";

export const RegisterCustomerPage = () => {
  const {
    authStore: { registerCustomerAction },
  } = useStore();

  const handleRegister = async (userRegister: UserRegister, profileRegister: ProfileRegister, customerRegister: CustomerRegister) => {
    await registerCustomerAction(userRegister, profileRegister, customerRegister);
  };

  return (
    <>
      <RegisterCustomerForm onRegister={handleRegister} />
    </>
  );
};
