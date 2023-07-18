import { Component, inject } from '@angular/core';
import { ApiService } from '../../../api/api.service';
import { ApiEndpointKey, ApiEndpoints} from '../../../api/api.model';
import { endpoints } from '../../../api/api-endpoints-map';
import { User } from '../../models/User';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})

export class UsersComponent {
  users: User[] = [];
  errorMessage: string = "";
  private readonly endpoints: ApiEndpoints;
  private apiService = inject(ApiService);

  public constructor() {
    this.endpoints = endpoints;
  }

  ngOnInit() {
     this.getItems();
  }

  getItems() {
    var response = this.apiService.get<any>(ApiEndpointKey.TODOITEMS).subscribe(
      {
        next: (response) => {
          this.users = response;
        },
        error: (error) => {
          this.errorMessage = "Error on loading items from backend server. Please make sure your host server is up running.";
        }
      }
    );
  }

  handleAdd(user: any) {
    if (!user.firstName || !user.lastName){
      this.errorMessage = "FirstName or lastName can not be empty";
      return;
    }

    this.errorMessage = "";
    const newUser = { firstName : user.firstName, lastName: user.lastName };
    var response = this.apiService.post<any, User>(ApiEndpointKey.TODOITEMS, newUser).subscribe(
        {
          next: (response) => {
            this.getItems();
            this.handleClear(user);
          },
          error: (error) => {
            // Handle any errors
          if (error?.status == 400 && error?.error){
            this.errorMessage = `Failed add new item due to: ${error?.error}`;
          }
          else{
            //internal server eror
            this.errorMessage = "Error on update item from backend server.";
          }
        }
      }
    );
  }

  handleClear(user: User) {
    user.firstName = '';
    user.lastName = '';
  }
}
