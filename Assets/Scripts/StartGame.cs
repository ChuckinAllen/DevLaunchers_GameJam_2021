using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;

public class StartGame : NetworkObjectFactoryBase
{
    private void MoveBall(Vector3 direction)
    {
        //Transform
    }
    protected override bool ValidateCreateRequest(NetWorker networker, int identity, uint id, FrameStream frame)
    {
        return base.ValidateCreateRequest(networker, identity, id, frame);
    }
}
