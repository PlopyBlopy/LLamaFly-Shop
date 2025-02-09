import { LogoButton } from "../../../features/logo-button";
import { useAppNavigate } from "../../../shared/routing/routes";
export const LogoRouterComponent = () => {
  const { goMain } = useAppNavigate();

  const onPageOpen = () => {
    goMain();
  };

  return (
    <>
      <LogoButton onPageOpen={onPageOpen} />
    </>
  );
};
