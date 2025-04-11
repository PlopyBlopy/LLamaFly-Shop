import { observer } from "mobx-react-lite";
import { ProfileSellerComponent } from "../../profile-seller-dropdown-menu";
import { ProfileCustomerComponent } from "../../profile-customer-dropdown-menu";
import { ProfileUnAuthorizedComponent } from "../../profile-unauthorized-dropdown-menu";
import { useEffect, useRef } from "react";
import { UserRoleType } from "../../../shared/services/profile-service";
import { useStore } from "../../../shared/hooks/store-hook";

export const ProfileComponent = observer(() => {
  const {
    profileStore: { profile, installUserRoleAction },
  } = useStore();

  const hasChecked = useRef(false);

  useEffect(() => {
    const checkUser = async () => {
      if (hasChecked.current) return;
      hasChecked.current = true;

      await installUserRoleAction();
    };

    checkUser();
  }, [profile]);

  const handleProfileDropdownVariant = () => {
    if (!profile) {
      return <ProfileUnAuthorizedComponent />;
    }

    switch (profile.role) {
      case UserRoleType.Seller:
        return <ProfileSellerComponent />;
      case UserRoleType.Customer:
        return <ProfileCustomerComponent />;
    }
  };

  return <>{handleProfileDropdownVariant()}</>;
});
