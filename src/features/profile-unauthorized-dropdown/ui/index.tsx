import { Button } from "@mui/material";
import { ProfileUnAuthorizedActions } from "../../../shared/profile-actions";

type Props = {
  onMenuAction: (action: ProfileUnAuthorizedActions) => void;
};

export const ProfileUnAuthorizedDropdown = ({ onMenuAction }: Props) => {
  return (
    <>
      <Button
        onClick={() => onMenuAction(ProfileUnAuthorizedActions.LOGIN)}
        variant="contained">
        Войти
      </Button>
      <Button
        onClick={() => onMenuAction(ProfileUnAuthorizedActions.REGISTER)}
        variant="contained">
        Зарегистрироваться
      </Button>
    </>
  );
};
