'use strict';

import leagueTpl from './league.html';

function leagueComponent($log) {
	'ngInject';

  var directive = {
    restrict: 'E',
    templateUrl: leagueTpl,
    controller: LeagueController,
    controllerAs: 'vm',
    bindToController: true
  };

  return directive;

  function LeagueController () {
	  $log.debug('Hello from League controller!');


    $(function() {
        //$.connection.hub.url = "";
        //var soccerHub = $.connection.soccerHub;
        this._events = [];
        var self = this;
        this.aa = "thuan";
        var connection = $.hubConnection("http://localhost:8082/s/hubs");
        var soccerHub = connection.createHubProxy("soccerHub");

         soccerHub.on('updateEvents', function(event){
                self._events.push(event);
            });

        connection.start().done(function(){
            console.log("connected: " + connection.id);
        
            soccerHub.invoke('getEvents').done(function(events){
                for (var i = 0; i < events.length; i++) {
                        self._events.push(events[i]);
                    }
            });
        });

    });
  }

}

export default leagueComponent;
