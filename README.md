# [Selection Outline]

## 1.설명
* Selection Outline Shader 제작
  - Normal 값 이용해서 아웃라인 그리는 쉐이더(각진 부분에 아웃라인이 끊기는 현상 발생)
  - Tangent 값 이용해서 아웃라인 그리는 쉐이더
    (각진 오브젝트들의 노말값의 평균을 구해 탄젠트 값에 넣어줌)
    (탄젠트 값에 넣어주는 이유 : 노말값에 덮어쓸 경우 빛의 방향과 노말값의 내적으로 빛을 계산하기 때문에 각진 부분이 어두워지는 현상 발생)
    
* Client : Unity 2017+

## 2. 이미지
* 적용 예시 - 왼쪽 normal값 이용한 쉐이더, 오른쪽 tangent값 이용한 쉐이더

![scene](https://blogfiles.pstatic.net/MjAxOTA4MTJfMjUy/MDAxNTY1NTk0NDc0MDIw.970cxN4zPCA2f5PbHrE2MPPmPx0oJ_muZhNyfRDY-vQg.8oxQ1djIHYKxaixAYiInytBLZp5AWoLZV5C8cgo_XFAg.PNG.gaebhi/outline.png?type=w1 "graph")

  
 
* 사용 예시

![sample](https://blogfiles.pstatic.net/MjAxOTA4MTJfMTIy/MDAxNTY1NTk0NDczNjk5.Dfo0WwJlpE1zpKVZtSZPh3YuGiZSaZ2coM5YRyEu6pwg.6IfA7OgQ2c4su13hSmGdc_00bOpb3llbsdzh2RhsARog.PNG.gaebhi/how_to_make_smooth_normal.png?type=w1 "sample")

  Editor에서 smoothed normal을 만들 오브젝트를 선택하고 
  menu의 Tools/Build Smoothed Normal을 눌러주면 Asssets/SmoothNormalMesh/ 폴더에 mesh가 생성된다.  
  mesh filter에 넣어주면 위 적용 예시와 같이 각진 부분의 아웃라인이 정상적으로 생성된다.
