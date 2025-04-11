import { useAppNavigate } from "../../../shared/routing/routes";
import { Result } from "antd";
import {
  ProfileCommonActions,
  ProfileCustomerActions,
} from "../../../shared/profile-actions";
import { ProfileCustomerDropdown } from "../../../features/profile-customer-dropdown";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../shared/hooks/store-hook";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import { Badge, IconButton } from "@mui/material";
import styles from "./index.module.css";

export const ProfileCustomerComponent = observer(() => {
  const {
    profileStore: { profile, profileError, logoutAction },
  } = useStore();
  const { goToMain, goToProfile, goToSettings } = useAppNavigate();

  if (profile == null) {
    return (
      <>
        <Result>{profileError}</Result>
      </>
    );
  }

  const handleMenuAction = (
    action: ProfileCommonActions | ProfileCustomerActions
  ) => {
    if (isCommon(action)) {
      switch (action) {
        case ProfileCommonActions.PROFILE:
          goToProfile(profile.id);
          break;
        case ProfileCommonActions.SETTINGS:
          goToSettings();
          break;
        case ProfileCommonActions.LOGOUT:
          logoutAction();
          goToMain();
          break;
      }
    } else if (isCustomer(action)) {
      switch (action) {
        case ProfileCustomerActions.ORDER_HISTORY:
          goToProfile(profile.id);
          break;
      }
    }
  };

  function isCommon(
    action: ProfileCommonActions | ProfileCustomerActions
  ): action is ProfileCommonActions {
    return Object.values(ProfileCommonActions).includes(
      action as ProfileCommonActions
    );
  }

  function isCustomer(
    action: ProfileCommonActions | ProfileCustomerActions
  ): action is ProfileCustomerActions {
    return Object.values(ProfileCustomerActions).includes(
      action as ProfileCustomerActions
    );
  }

  return (
    <div className={styles.icons}>
      <IconButton color="inherit">
        <Badge badgeContent={4} color="error">
          <ShoppingCartIcon />
        </Badge>
      </IconButton>
      <ProfileCustomerDropdown onMenuAction={handleMenuAction} />
    </div>
  );
});
