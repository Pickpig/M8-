
var _setTabinfo;
var _flashparacall;
var _currTabId;
var _currTabIndex;
var _tabarr;
var _startDate;
var _endDate;
//var _tabId;//tabbar��������id
//�õ��Զ���Label������չʾ
function getScxnLabelClickDiv(id, icon, title, image, content, tabarr, flashparacall) {
    //id:���������id;tab:�����tabbar;setTabinfo:����tabbar��ʾ������
	_flashparacall = flashparacall;
	_tabarr = tabarr;
	_currTabId = id;
    var tabinfocall = function(index, setTabinfo) {
		_setTabinfo = setTabinfo;
		_currTabIndex = index;
		_startDate=formatDate(getDate('d',-1));
		_endDate=formatDate(getDate());
		
		if(_tabarr[_currTabIndex].name=="����ͼ"){
			_startDate=formatDate(getDate());
		}
		//_flashparacall(_tabarr[_currTabIndex],_startDate,_endDate);
        //setTabinfo(getFlashHtml(tabarr[index].swfUrl, flashparacall(id, tabarr[index],formatDate(getDate('d',-1)),formatDate(getDate())), 400, 200));
		//setTimeout('refreshTabinfo(formatDate(getDate("d",-4)),formatDate(getDate()));',3000);
		
    };
	var tabarrs = new Array();
	for(var i = 0;i<tabarr.length;i++){
		tabarrs.push(tabarr[i].name);
	}
    var d = new ScxnLabelDiv(icon, title, image, content, tabarrs, tabinfocall).getdiv();

    return d;
}
function getLabelValue(id){
	var type = getTypeId('ˮ��վ');
	var dataInfos = dataMap.get(type);
	for(var i=0;i<dataInfos.length;i++){
		if(dataInfos[i].id==id){
			var sct = dataInfos[i].simpleContent;
			var ct = dataInfos[i].content;
			var unit = getUnitByType(type);
			return ct.substring(sct.length+1,ct.indexOf(unit));
		}
	}
}

//����tabbar����
function setTabbarInfo(para){
	var mpd = getDataPointMap().get(_currTabId);
	var tbb = mpd.tabbars[_currTabIndex];
	var unit = tbb.unit.split(",");
	var chartUnit = tbb.chartUnit.split(",");
	var chartType = tbb.chartType.split(",");
	var left = "";
	var right = "";
	if(unit.length==2){
		left = "left,linear,"+chartUnit[0]+","+unit[0]+","+chartType[0]+",v1";
		right = "@right,linear,"+chartUnit[1]+","+unit[1]+","+chartType[1]+",v2";
	}else{
		
		left = "left,linear,"+chartUnit.join("#")+","+unit[0]+","+chartType.join("#")+","+chartUnit.join("#");
	}
	var p;
	if(para!="")
	{
		 p = mpd.title+";"+_startDate+","+_endDate+";"+left+right+";bottom,datatime,ʱ��,(Сʱ),tm;100,0,20@100,0,20@2014-01-04 17:00:00,2014-01-03 17:00:00,4;"+para[0];
	}else{
		 p = mpd.title+";"+_startDate+","+_endDate+";"+left+right+";bottom,datatime,ʱ��,(Сʱ),tm;100,0,20@100,0,20@2014-01-04 17:00:00,2014-01-03 17:00:00,4;";
	}
	_setTabinfo(getFlashHtml(_tabarr[_currTabIndex].swfUrl, p, 420, 210));
}

//�õ��ӵ�չʾ��div
function getStreamClickDiv(content) {
    var d = document.createElement("div");
	d.setAttribute("style","white-space:nowrap;color:#ffffff;font-size:10;width:125px;height:28px;text-align:center;background:url('images/tishikuang.png') repeat;padding:10px 27px 10px 28px;");
	d.innerHTML = content;
    return d;
}
//�õ�flash����
function getFlashHtml(src, content, width, height) {
    var flash = "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0' width='" + width + "' height='" + height + "' id='flexChartObject' align='middle'> <param name='allowScriptAccess' value='sameDomain' /> <param name='allowFullScreen' value='false' /> <PARAM NAME='flashvars' VALUE='"+content+"'> <param name='movie' value='"+src+"' /><param name='quality' value='high' /><param name='bgcolor' value='#ffffff' />	<embed src='"+src+"' mce_src='" + src + "' flashvars='" + content + "' quality='high' bgcolor='#ffffff' width='"+width+"' height='"+height+"' id='flexChartEmbed' align='middle' allowScriptAccess='sameDomain' allowFullScreen='false' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /> </object> ";
    return flash;
}
//ˢ��ͼ��
function refreshTabinfo(startDate,endDate){
	if(_setTabinfo&&_flashparacall&&_currTabIndex!=null&&_tabarr){
		_startDate = startDate;
		_endDate = endDate;
		_flashparacall(_tabarr[_currTabIndex],startDate,endDate);
		//_setTabinfo(getFlashHtml(_tabarr[_currTabIndex].swfUrl, _flashparacall(_currTabId, _tabarr[_currTabIndex],startDate,endDate), 400, 200));
	}
}
//�õ�ʱ��
function getDate(strInterval, nb) {   
    var dtTmp = new Date();
	if(strInterval!=null&&nb!=null){
		switch (strInterval) {   
			case 's' :return new Date(dtTmp.getTime() + (1000 * nb));  
			case 'n' :return new Date(dtTmp.getTime() + (1000 * nb * 60));  
			case 'h' :return new Date(dtTmp.getTime() + (1000 * nb * 60 * 60));  
			case 'd' :return new Date(dtTmp.getTime() + (1000 * nb * 60 * 60 * 24));  
			case 'w' :return new Date(dtTmp.getTime() + (1000 * nb * 60 * 60 * 24 * 7));  
			case 'q' :return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + nb*3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());  
			case 'm' :return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + nb, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());  
			case 'y' :return new Date((dtTmp.getFullYear() + nb), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());  
		} 
	}
	return dtTmp;
}  
function formatDate(dt){
	if(dt instanceof Date){
		var y = dt.getFullYear();
		var m = (dt.getMonth()+1)>9?(dt.getMonth()+1):"0"+(dt.getMonth()+1);
		var d = dt.getDate()>9?dt.getDate():"0"+dt.getDate();
		var hh = dt.getHours(); //��ȡСʱ 
		var mm = dt.getMinutes(); //��ȡ����
		
		return y+"-"+m+"-"+d+" "+hh+":"+mm;
	}
}
//����Flex�ķ���,���ط�������ֵ
function callFlex(id,call,para){
	if(id==null||call==null||(document.getElementById(id+"Object")==null&&document.getElementById(id+"Embed")==null)){
		alert("����,û���ҵ�flash����,������."+id+","+call);
		return "";
	}
	if(call in document.getElementById(id+"Object")){
		if(para==null){
			return document.getElementById(id+"Object")[call]();
		}else{
			return document.getElementById(id+"Object")[call](para);
		}
	}else if(call in document.getElementById(id+"Embed")){
		if(para==null){
			return document.getElementById(id+"Embed")[call]();
		}else{
			return document.getElementById(id+"Embed")[call](para);
		}
	}
}
//��Ӹ������,�����ظ�����
function mapAddPolygon(map, points, lineOpacity, lineColor, fillOpacity, fillColor, strokeWeight) {
    if (strokeWeight == null) {
        strokeWeight = 1;
    }
        var ply = new BMap.Polygon(points, {
            strokeWeight: strokeWeight,
            strokeColor: fillColor
        });
        ply.setFillOpacity(fillOpacity);
        ply.setFillColor(fillColor);
        ply.setStrokeOpacity(lineOpacity);
        map.addOverlay(ply);
		return ply;
}
//��Ӷ���������
function mapAddPolygons(map, points, lineOpacity, lineColor, fillOpacity, fillColor, strokeWeight) {
    if (strokeWeight == null) {
        strokeWeight = 1;
    }
    for (var i = 0; i < points.length; i++) {
		mapAddPolygon(map, points[i], lineOpacity, lineColor, fillOpacity, fillColor, strokeWeight);
    }
}
//��Ӹ�������,�����ظ�����
function mapAddPolyline(map, points, lineColor, lineOpacity, strokeWeight) {
    if (strokeWeight == null) {
        strokeWeight = 1;
    }
        var pl = new BMap.Polyline(points, {
            strokeColor: lineColor,
            strokeOpacity: lineOpacity,
            strokeWeight: strokeWeight
        });
        map.addOverlay(pl);
		return pl;
}
//��Ӷ����������
function mapAddPolylines(map, points, lineColor, lineOpacity, strokeWeight) {
    if (strokeWeight == null) {
        strokeWeight = 1;
    }
    for (var i = 0; i < points.length; i++) {
		mapAddPolyline(map, points[i], lineColor, lineOpacity, strokeWeight);
    }
}
//�õ���Ұ��������������
function getBoundary(map, linecolor, fillcolor, fillopacity, callback, strokeWeight) {
    if (strokeWeight == null) {
        strokeWeight = 1;
    }
    var boundary = new BMap.Boundary();
	var fx = 0;
	var fy = 0;
    //boundary.get(address,
    //function(rs) { //��ȡ��������
        //var count = rs.boundaries.length; //��������ĵ㼯���ж��ٸ�
        //for (var i = 0; i < count; i++) {
			//document.getElementById("info_div").innerHTML += "<br/>"+i+"��:"+rs.boundaries[i];
			var boundariesArray = getBoundaries();//rs.boundaries[i]
            var ply = new BMap.Polyline(boundariesArray, {	
                strokeWeight: strokeWeight,
                strokeColor: linecolor,
				strokeStyle: "dashed"
            }); //��������θ�����
            map.addOverlay(ply); //��Ӹ�����
            //if (i == 0) {
                map.setViewport(ply.getPath()); //������Ұ
                map.setZoom(map.getZoom() - 2);
                //���㶫�������ĸ����㾭γ��
                var bs = map.getBounds(); //��ȡ��������
                var north = bs.getNorthEast().lat; //��
                var south = bs.getSouthWest().lat; //��
                var west = bs.getSouthWest().lng; //��
                var east = bs.getNorthEast().lng; //��
                var mapMaxX = east > west ? east: west;
                var mapMinX = east > west ? west: east;
                //�������߽�
                var boundaries = boundariesArray.split(";");
                var maxX;
                var maxXIndex = 0;
                var minX;
                var minXIndex = 0;
                var maxY;
                var minY;
                for (var k = 0; k < boundaries.length; k++) {
                    var point = boundaries[k].split(",");
                    if (!maxX) {
                        maxX = point[0];
                        minX = point[0];
                        maxY = point[1];
                        minY = point[1];
                    } else {
                        if (maxX * 1 < point[0] * 1) {
                            maxX = point[0];
                            maxXIndex = k;
                        } else if (minX * 1 > point[0] * 1) {
                            minX = point[0];
                            minXIndex = k;
                        }
                        maxY = maxY * 1 > point[1] * 1 ? maxY: point[1];
                        minY = minY * 1 < point[1] * 1 ? minY: point[1];
                    }
                }
                var start = maxXIndex > minXIndex ? minXIndex: maxXIndex;
                var end = maxXIndex > minXIndex ? maxXIndex: minXIndex;
                var polygonA = [180 + "," + 180 ,0 + "," + 180, 0 + "," + 0];
                var polygonB = [ 0 + "," + 0, 180 + "," + 0,180 + "," + 180];
                var polygonA = polygonA.concat(boundaries.slice(start, end + 1));
                var polygonB = polygonB.concat(boundaries.slice(end, boundaries.length).concat(boundaries.slice(0, start + 1)));
                mapAddPolygon(map, polygonA.join(";"), 0.01, fillcolor, fillopacity, fillcolor);
                mapAddPolygon(map, polygonB.join(";"), 0.01, fillcolor, fillopacity, fillcolor);
            //}
            map.setViewport(ply.getPath()); //������Ұ
            //�����϶�����
			if (isAreaRestriction()) {
				BMapLib.AreaRestriction.setBounds(map, map.getBounds());
            }
            if (callback) {
                callback();
            }
        //}
    //});
}

function addlog(v) {
    document.getElementById("info_div").innerHTML += v;
}