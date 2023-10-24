export class RegisterLoginComponentModel{
    firstName : string;
    lastName : string;
    userName : string;
    password : string;
    email : string;

    constructor(firstName : string, lastName : string, userName : string, password : string, email : string){
        this.firstName = firstName;
        this.lastName = lastName;
        this.userName = userName;
        this.password = password;
        this.email = email;
    }
}