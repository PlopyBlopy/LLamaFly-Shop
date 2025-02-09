import { makeAutoObservable } from "mobx";
import { Customer, Seller } from "../../../../shared/profile-service-profiles";
import { DEFAULT_CUSTOMER_PROFILE_ID, DEFAULT_SELLER_PROFILE_ID } from "../";

export default class ProfileStore
{
    profile: Seller | Customer | null = null;
    profileError: string = "";
    isSeller: boolean = false;

    constructor(){
        makeAutoObservable(this);
        this.switchProfile();
    }

    switchProfile = () =>
    {
        if(this.isSeller)
            this.getSellerProfileAction(DEFAULT_SELLER_PROFILE_ID)
        else  
            this.getCustomerProfileAction(DEFAULT_CUSTOMER_PROFILE_ID)

        this.isSeller = !this.isSeller;
    }

    getSellerProfileAction = async (id: string) =>
    {
        const newSeller: Seller = {
            id: id, 
            name: "Seller",
        };
        this.profile = newSeller;
    }

    getCustomerProfileAction = async (id: string) =>
    {
        const newCustomer: Customer = {
            id: id, 
            name: "Customer",
        };
        this.profile = newCustomer;
    }
}