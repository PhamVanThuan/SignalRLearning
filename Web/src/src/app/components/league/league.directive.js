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
        var _events = [];


        var connection = $.hubConnection("http://localhost:8082/s");
        var soccerHub = connection.createHubProxy("soccerHub");

        

        connection.start().done(function(){
           console.log(connection.id);
        });

    });


  }

}

export default leagueComponent;
