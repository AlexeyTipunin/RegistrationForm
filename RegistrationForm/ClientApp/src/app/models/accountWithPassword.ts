import { Account } from "./Account";

export class AccountWithPassword extends Account {
    password: string;
    passwordConfirmation: string;
}
