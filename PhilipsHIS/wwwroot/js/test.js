angular.module('testApp', [])
  .controller('TestController', function() {
    var myTest = this;
    var columns = 4;
    myTest.cell_save = 4;

    myTest.render = function(){
      myTest.cell_save = myTest.cell_set;
      myTest.rows = myTest.getRow();
    }

    myTest.getRow = function(){
      var row_count = Math.floor(myTest.cell_save/columns);
      var last_row = myTest.cell_save%columns;
      console.log(row_count,last_row);
      var ret = [];
      for(i =0;i<row_count;i++){
        ret.push(columns);
      }
      if(last_row>0)ret.push(last_row);
      return ret;
    }

    myTest.rows = myTest.getRow();

    myTest.getCell = function(cell){
      console.log(cell);
      var ret = [];
      for(i =0;i<cell;i++){
        ret.push(i);
      }
      return ret;
    }
  });
