import { AuthService } from '@auth0/auth0-angular';
import { UserService } from './../user.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.scss']  // Correct the property name to 'styleUrls'
})
export class ConnectionComponent {
  public email: string = '';
  public password: string = '';

  constructor(private _userService: UserService, public _auth: AuthService) {}

  loginUser(): void 
  {
    // Call the UserLogin method from UserService
    this._userService.UserLogin(this.email, this.password).subscribe(response => 
    {
      // Log the token now BITCH
      console.log('JWT Token:', response.token);
    }, error => 
    {
    
      console.error('Error during login:', error);
    });
  }
}
