import { Component, OnInit } from '@angular/core';
declare var $:any;

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  name = "thuan";
  events = [];
  a = [1,2,3,4];

  constructor() {
    var self = this;
    var connection = $.hubConnection("http://localhost:8082/s/hubs");
        var soccerHub = connection.createHubProxy("soccerHub");

         soccerHub.on('updateEvents', function(event){
           console.log(event);
                  self.events.push(event);
                  console.log(event);
            });

        connection.start().done(function(){
            console.log("connected: " + connection.id);
        
            soccerHub.invoke('getEvents').done(function(events){
                for (var i = 0; i < events.length; i++) {
                        self.events.push(events[i]);
                    }
            });
        });
   }

  ngOnInit() {
    
  }

}
