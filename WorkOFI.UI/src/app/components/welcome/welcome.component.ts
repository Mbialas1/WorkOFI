import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/user.service';
import { LoginComponentModel } from 'src/app/models/loginComponent.model';
import { RegisterLoginComponentModel } from 'src/app/models/registerLoginComponents';

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent {

    constructor(private userService: UserService, private router : Router){}

    showLogin = true;

    loginUsername = '';
    loginPassword = '';

    registerFirstName = '';
    registerLastName = '';
    registerEmail = '';
    registerUsername = '';
    registerPassword = '';
    confirmPassword = '';

    handleLogin() {
        const loginData = new LoginComponentModel(this.loginUsername, this.loginPassword);

        this.userService.loginToApplication(loginData).subscribe(result => {
            if (result) {
                this.router.navigate(['/dashboard']);
            }
            
            alert('Inncorect login or password!');
        });
    }

    handleRegister() {
        if (this.registerPassword !== this.confirmPassword) {
            alert('Password is not this same');
            return;
        }

        const registerData = new RegisterLoginComponentModel(this.registerFirstName, this.registerLastName, this.registerUsername, this.registerPassword, this.registerEmail);

        this.userService.registerToApplication(registerData).subscribe(result => {
            if (result) {
                this.showLogin = true;
            }
            alert('Inncorect data!');
        });
    }

    switchToRegister() {
        this.showLogin = false;
    }

    switchToLogin() {
        this.showLogin = true;
    }
}
