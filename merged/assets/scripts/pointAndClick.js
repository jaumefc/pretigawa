#pragma strict

var speed:int; // Determines how quickly object moves towards position


private var targetPosition:Vector3;

function Update () {
    if(Input.GetKeyDown(KeyCode.Mouse0))
    {
        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        var hitdist = 0.0;
        
        if (playerPlane.Raycast (ray, hitdist)) {
            var targetPoint = ray.GetPoint(hitdist);
            targetPosition = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;
        }
    }
    
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
}