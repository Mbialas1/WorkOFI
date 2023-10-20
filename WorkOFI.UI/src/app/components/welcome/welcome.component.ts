import { Component } from '@angular/core';

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent {
    showLogin = true;

    // Dane logowania
    loginUsername = '';
    loginPassword = '';

    // Dane rejestracji
    registerUsername = '';
    registerPassword = '';
    confirmPassword = '';

    handleLogin() {
        // Tutaj obsługuje logowanie
    }

    handleRegister() {
        // Tutaj obsługuje rejestrację
    }

    switchToRegister() {
        this.showLogin = false;
    }

    switchToLogin() {
        this.showLogin = true;
    }
}
