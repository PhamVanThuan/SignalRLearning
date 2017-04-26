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
        $.connection.hub.url = "http://localhost:8082/s";
        var soccerHub = $.connection.soccerHub;
        var _events = [];

        soccerHub.client.updateEvents = function(data) {
            console.log("added: " + data.EventId);
            _events.push(data);
        }

        soccerHub.client.removeEvents = function (data) {
            console.log("remove: " + data.EventId);
        }

        $.connection.hub.start().then(init);

        function init() {
            return soccerHub.server.getEvents()
                .done(function(events) {
                    for (var i = 0; i < events.length; i++) {
                        _events.push(events[i]);
                        console.log(events[i].EventId);
                    }
                });
        }

    });


  }

}

export default leagueComponent;
