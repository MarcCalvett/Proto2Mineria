using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fraktalia.VoxelGen.Visualisation
{
	[System.Serializable]
	public struct DetailPlacement
	{
		public int MinID;
		public int MaxID;
		[Range(0,1)]
		public float FallOff;

		public float CalculateDetail(float fX, float fY, float fZ, ref NativeVoxelTree dataset)
		{
			float value = 1;

			int x = VoxelGenerator.ConvertLocalToInner(fX, dataset.RootSize);
			int y = VoxelGenerator.ConvertLocalToInner(fY, dataset.RootSize);
			int z = VoxelGenerator.ConvertLocalToInner(fZ, dataset.RootSize);

			int ID = dataset._PeekVoxelId_InnerCoordinate(x, y, z, 10,0,0,1);

			if(ID > MaxID)
			{
				float difference = ID - MaxID;
				value = 1 - difference * FallOff; 
			}
			else if(ID < MinID)
			{
				float difference = MinID - ID;
				value = 1 - difference * FallOff;
			}

			
			return Mathf.Clamp01(value);
		}

		public float CalculateLife(float fX, float fY, float fZ, ref NativeVoxelTree dataset)
		{
			int x = VoxelGenerator.ConvertLocalToInner(fX, dataset.RootSize);
			int y = VoxelGenerator.ConvertLocalToInner(fY, dataset.RootSize);
			int z = VoxelGenerator.ConvertLocalToInner(fZ, dataset.RootSize);

			int ID = dataset._PeekVoxelId_InnerCoordinate(x, y, z, 10, 0, 0, 1);
			return Mathf.Clamp01(ID/256f);
		}

		internal float GetChecksum()
		{
			return MinID + MaxID + FallOff*100;
		}
	}
}
