import { useAppNavigate } from "../../../shared/routing/routes";
import { Result } from "antd";
import {
  ProfileCommonActions,
  ProfileSellerActions,
} from "../../../shared/profile-actions";
import { ProfileSellerDropdown } from "../../../features/profile-seller-dropdown";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../shared/hooks/store-hook";
import StorefrontIcon from "@mui/icons-material/Storefront";
import styles from "./index.module.css";
import { Badge, IconButton } from "@mui/material";

export const ProfileSellerComponent = observer(() => {
  const {
    goToMain,
    goToProfile,
    goToSettings,
    goToSellerProducts,
    goToAddProduct,
  } = useAppNavigate();
  const {
    profileStore: { profile, profileError, logoutAction },
  } = useStore();

  if (profile == null) {
    return (
      <>
        <Result>{profileError}</Result>
      </>
    );
  }

  const handleMenuAction = (
    action: ProfileCommonActions | ProfileSellerActions
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
    } else if (isSeller(action)) {
      switch (action) {
        case ProfileSellerActions.MY_PRODUCTS:
          goToSellerProducts();
          break;
        case ProfileSellerActions.ADD_PRODUCT:
          goToAddProduct();
          break;
      }
    }
  };

  function isCommon(
    action: ProfileCommonActions | ProfileSellerActions
  ): action is ProfileCommonActions {
    return Object.values(ProfileCommonActions).includes(
      action as ProfileCommonActions
    );
  }

  function isSeller(
    action: ProfileCommonActions | ProfileSellerActions
  ): action is ProfileSellerActions {
    return Object.values(ProfileSellerActions).includes(
      action as ProfileSellerActions
    );
  }

  return (
    <div className={styles.icons}>
      <IconButton
        color="inherit"
        onClick={() => handleMenuAction(ProfileSellerActions.MY_PRODUCTS)}>
        <Badge>
          <StorefrontIcon />
        </Badge>
      </IconButton>
      <ProfileSellerDropdown onMenuAction={handleMenuAction} />
    </div>
  );
});
