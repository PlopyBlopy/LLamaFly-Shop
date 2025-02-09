import { useAppNavigate } from "../../../shared/routing/routes";
import { useStore } from "../../../shared/contexts/store-context";

import { Result } from "antd";
import {
  ProfileCommonActions,
  ProfileCustomerActions,
  ProfileSellerActions,
} from "../../../shared/profile-actions";
import { ProfileSellerDropdown } from "../../../features/profile-seller-dropdown";
import { ProfileCustomerDropdown } from "../../../features/profile-customer-dropdown";
import { observer } from "mobx-react-lite";

export const ProfileComponent = observer(() => {
  const { goToProfile, goToSettings, goToAddProduct } = useAppNavigate();
  const rootStore = useStore();
  const {
    profileSellerStore: { profile, profileError, isSeller, switchProfile },
  } = rootStore;

  if (profile == null) {
    <>
      <Result>{profileError}</Result>
    </>;
    return;
  }
  const handleMenuAction = (
    action: ProfileCommonActions | ProfileSellerActions | ProfileCustomerActions
  ) => {
    if (isCommonAction(action)) {
      switch (action) {
        case ProfileCommonActions.PROFILE:
          goToProfile(profile.id);
          break;
        case ProfileCommonActions.SWITCH_ROLE:
          switchProfile();
          break;
        case ProfileCommonActions.SETTINGS:
          goToSettings();
          break;
      }
    } else if (isSellerAction(action)) {
      switch (action) {
        case ProfileSellerActions.ADD_PRODUCT:
          goToAddProduct();
          break;
      }
    } else if (isCustomerAction(action)) {
      switch (action) {
        case ProfileCustomerActions.ORDER_HISTORY:
          goToProfile(profile.id);
          break;
      }
    }
  };

  // Проверка, является ли действие действием общим для продавца и покупателя
  function isCommonAction(
    action: ProfileCommonActions | ProfileSellerActions | ProfileCustomerActions
  ): action is ProfileCommonActions {
    return Object.values(ProfileCommonActions).includes(
      action as ProfileCommonActions
    );
  }
  // Проверка, является ли действие действием продавца
  function isSellerAction(
    action: ProfileCommonActions | ProfileSellerActions | ProfileCustomerActions
  ): action is ProfileSellerActions {
    return Object.values(ProfileSellerActions).includes(
      action as ProfileSellerActions
    );
  }
  // Проверка, является ли действие действием покупателя
  function isCustomerAction(
    action: ProfileCommonActions | ProfileSellerActions | ProfileCustomerActions
  ): action is ProfileCustomerActions {
    return Object.values(ProfileCustomerActions).includes(
      action as ProfileCustomerActions
    );
  }

  return (
    <>
      {isSeller ? (
        <ProfileSellerDropdown onMenuAction={handleMenuAction} />
      ) : (
        <ProfileCustomerDropdown onMenuAction={handleMenuAction} />
      )}
    </>
  );
});
