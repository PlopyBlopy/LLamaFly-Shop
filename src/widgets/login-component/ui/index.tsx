import { LoginUserForm } from "../../../features/login-form";
import { useAppNavigate } from "../../../shared/routing/routes";
import { UserLogin } from "../../../shared/services/auth-service-user";
import { useStore } from "../../../shared/hooks/store-hook";

export const LoginComponent = () => {
  const {
    authStore: { loginAction },
    profileStore: { installUserRoleAction },
  } = useStore();
  const { goToMain } = useAppNavigate();

  const handleLogin = async (user: UserLogin) => {
    await loginAction(user);
    await installUserRoleAction();
    await goToMain();
  };

  return <LoginUserForm onLogin={handleLogin} />;
};
