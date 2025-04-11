import { useAppNavigate } from "../../../shared/routing/routes";

import { ProfileUnAuthorizedActions } from "../../../shared/profile-actions";
import { observer } from "mobx-react-lite";
import { ProfileUnAuthorizedDropdown } from "../../../features/profile-unauthorized-dropdown";

export const ProfileUnAuthorizedComponent = observer(() => {
  const { goToRegister, goToLogin } = useAppNavigate();

  const handleMenuAction = (action: ProfileUnAuthorizedActions) => {
    if (isUnAuthorized(action)) {
      switch (action) {
        case ProfileUnAuthorizedActions.LOGIN:
          goToLogin();
          break;
        case ProfileUnAuthorizedActions.REGISTER:
          goToRegister();
          break;
      }
    }
  };

  function isUnAuthorized(
    action: ProfileUnAuthorizedActions
  ): action is ProfileUnAuthorizedActions {
    return Object.values(ProfileUnAuthorizedActions).includes(
      action as ProfileUnAuthorizedActions
    );
  }

  return (
    <>
      <ProfileUnAuthorizedDropdown onMenuAction={handleMenuAction} />
    </>
  );
});
